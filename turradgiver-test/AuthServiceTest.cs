using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System;
using Xunit;
using Moq;
using AutoMapper;
using Newtonsoft.Json;

using turradgiver_dal.Models;
using turradgiver_dal.Repositories;

using turradgiver_bal.Services;
using turradgiver_bal.Mappers;
using turradgiver_bal.Dtos.User;
using turradgiver_bal.Dtos.Auth;

namespace turradgiver_test
{
    public class AuthServiceTest
    {
        private readonly AuthService _sut;
        private readonly DbContextFixture _dbContext;
        private readonly IRepository<User> _customUserRepo;
        private readonly IRepository<RefreshToken> _customRefreshTokenRepo;

        private readonly Mock<IConfiguration> _customConfigMock = new Mock<IConfiguration>();
        private readonly Mock<IJwtService> _customJWTServMock = new Mock<IJwtService>();

        public AuthServiceTest(){
            // IRepository<User> userRepository, IConfiguration configuration,IRepository<RefreshToken> refreshTokenRepository, ILogger<AuthService> logger
            _dbContext = new DbContextFixture();
            _dbContext.Database.EnsureCreated();
            _customUserRepo =  new Repository<User>(_dbContext);
            _customRefreshTokenRepo = new Repository<RefreshToken>(_dbContext);
            
            // _customJWTService = new JwtService(_customConfigMock.Object);
            _sut = new AuthService(_customUserRepo,_customConfigMock.Object, _customRefreshTokenRepo, _customJWTServMock.Object);
        }



        [Fact]
        public async Task Register_WhenEmailDoesNotExist(){
            //Arrange
            var signUpDto = new UserSignUpDto{
                Email="kermit@kermit.com",
                Username = "Kermit",
                Password = "Kermit"
            };

            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
            // Mock this methods even if the output of GenerateToken is change because it will result with an exception for nullref on an object 
            _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);

            // Act
            var resRegister =  await _sut.RegisterAsync(signUpDto);
            
            // Assert
            Assert.NotNull(resRegister.Data);
            Assert.True(resRegister.Success);
            Assert.Equal(resRegister.Data.Token, tok.Token);
            Assert.Equal(resRegister.Data.RefreshToken, tok.Token);
        }
        
        [Fact]
        public async Task Register_WhenEmailExist(){
            //Arrange
            var signUpDto = new UserSignUpDto{
                Email="babidiii@babidiii.babidiii",
                Username = "Kermit",
                Password = "Kermit"
            };

            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
            // Mock this methods even if the output of GenerateToken is change because it will result with an exception for nullref on an object 
            _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);

            // Act
            var resRegister =  await _sut.RegisterAsync(signUpDto);
            
            // Assert
            Assert.Null(resRegister.Data);
            Assert.False(resRegister.Success);
        }


        [Fact]
        public async Task Login_WhenUserExists()
        {
            // Arrange
            string email = "babidiii@babidiii.babidiii";
            string pwd = "Babidiii";

            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
            
             _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);
            
            // Act
            var resLogin =  await _sut.LoginAsync(email, pwd);

            // // Assert
            Assert.NotNull(resLogin.Data);
            Assert.True(resLogin.Success);
            Assert.Equal(resLogin.Data.Token, tok.Token );
            Assert.Equal(resLogin.Data.RefreshToken, tok.Token );
        }

        [Fact]
        public async Task Login_WhenUserDoesNotExist()
        {
            // Arrange
            string email = "Je@suis.japonaiiiiiiiiiiiis";
            string pwd = "Babitoc";

            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
            
             _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);
            
            // Act
            var resLogin =  await _sut.LoginAsync(email, pwd);

            // // Assert
            Assert.Null(resLogin.Data);
            Assert.False(resLogin.Success);
        }

        [Fact]
        public async Task Login_WhenUserPasswordIsIncorrect()
        {
            // Arrange
            string email = "babidiii@babidiii.babidiii";
            string pwd = "Babitoc";
            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
            
             _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);
            
            // Act
            var resLogin =  await _sut.LoginAsync(email, pwd);

            // // Assert
            Assert.Null(resLogin.Data);
            Assert.False(resLogin.Success);
        }

        [Fact]
        public async Task RefreshToken_WhenTokenIsInvalid()
        {
            // Arrange
            string refreshToken = "MyBadRefreshToken";
            var refreshTokenDto = new ExchangeRefreshTokenDto {
                RefreshToken = refreshToken
            };
            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
 
             _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);
            _customJWTServMock.Setup( x => x.ValidateToken(It.IsAny<string>(),It.IsAny<byte[]>())).Returns(false);
            
            // Act
            var resRefreshToken =  await _sut.RefreshToken(refreshTokenDto);

            // // Assert
            Assert.Null(resRefreshToken.Data);
            Assert.False(resRefreshToken.Success);
        }

        [Fact]
        public async Task RefreshToken_WhenTokenIsNotInTheDatabase()
        {
            // Arrange
            string refreshToken = "MyBadRefreshToken";
            var refreshTokenDto = new ExchangeRefreshTokenDto {
                RefreshToken = refreshToken
            };
            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
 
             _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);
            _customJWTServMock.Setup( x => x.ValidateToken(It.IsAny<string>(),It.IsAny<byte[]>())).Returns(true);
            
            // Act
            var resRefreshToken =  await _sut.RefreshToken(refreshTokenDto);

            // // Assert
            Assert.Null(resRefreshToken.Data);
            Assert.False(resRefreshToken.Success);
        }

        [Fact]
        public async Task RefreshToken_WhenTokenValid()
        {
            // Arrange
            string refreshToken = "babidiii-token";
            var refreshTokenDto = new ExchangeRefreshTokenDto {
                RefreshToken = refreshToken
            };
            var tok = new TokenDto (){
                Token = "MyToken",
                Expires = DateTime.Now
            };
 
             _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkeyyyyyyyyyyyyyyyyyyyyyyy");
            _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);
            _customJWTServMock.Setup( x => x.ValidateToken(It.IsAny<string>(),It.IsAny<byte[]>())).Returns(true);
            
            // Act
            var resRefreshToken =  await _sut.RefreshToken(refreshTokenDto);

            // // Assert
            Assert.NotNull(resRefreshToken.Data);
            Assert.True(resRefreshToken.Success);
            Assert.Equal(resRefreshToken.Data.Token, tok.Token );
            Assert.Equal(resRefreshToken.Data.RefreshToken, tok.Token );
        }      
    }
}
