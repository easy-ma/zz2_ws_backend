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
        private readonly IJwtService _customJWTService;

        public AuthServiceTest(){
            // IRepository<User> userRepository, IConfiguration configuration,IRepository<RefreshToken> refreshTokenRepository, ILogger<AuthService> logger
            _dbContext = new DbContextFixture();
            _dbContext.Database.EnsureCreated();
            _customUserRepo =  new Repository<User>(_dbContext);
            _customRefreshTokenRepo = new Repository<RefreshToken>(_dbContext);
            
            _customJWTService = new JwtService(_customConfigMock.Object);
            _sut = new AuthService(_customUserRepo,_customConfigMock.Object, _customRefreshTokenRepo, _customJWTService);
        }



        [Fact]
        public async Task Register_WhenEmailDoesNotExist(){
            //Arrange
            var signUpDto = new UserSignUpDto{
                Email="kermit@kermit.com",
                Username = "Kermit",
                Password = "Kermit"
            };
            
            // Act
            var resRegister =  await _sut.RegisterAsync(signUpDto);
            _customConfigMock.Setup( x => x.GetSection(It.IsAny<string>()).Value).Returns("jwtkey");

            // Assert
            Assert.NotNull(resRegister.Data);
            Assert.True(resRegister.Success);
        }

        // [Fact]
        // public async Task Login_WhenUserExists()
        // {
        //     // Arrange
        //     // Task<Response<AuthCredentialDto>> RegisterAsync(UserSignUpDto userSignUpDto);
        //     // Task<Response<AuthCredentialDto>> LoginAsync(string email, string password);
        //     // Task<Response<AuthCredentialDto>> RefreshToken(ExchangeRefreshTokenDto refreshDto);
            
        //     // Guid id = new Guid("b4ae46e1-41a3-49a2-bf59-76ced244cd30"); 
        //     // string username= "Babidiii";
        //     string email = "babdiii@babidiii.babidiii";
        //     string pwd = "Babidiii";

        //     // var user = new User(username, email){
        //     //     Id= id,
        //     //     Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pwd))
        //     // };
        //     // List<User> users = new List<User>() {user};
        //     // users.Add(user);
        //     // users.Add(new User("test","test"));
        //     // IQueryable<User> queryableUsers = users.AsQueryable();
        //     var tok = new TokenDto (){
        //         Token = "MyToken",
        //         Expires = DateTime.Now
        //     };
            
        //     // Mock<AuthService> _customAuthServiceMock = new Mock<AuthService>();
        //     // _customUserRepoMock.Setup( x => x.GetByConditionAsync(It.IsAny<Expression<Func<User, bool>>>() )).ReturnsAsync(queryableUsers);
        //     // _customJWTServMock.Setup( x => x.GenerateToken(It.IsAny<byte[]>(),It.IsAny<double>(),It.IsAny<IEnumerable<Claim>>())).Returns(tok);
        //     // byte[] secretKey,double expMinutes,IEnumerable<Claim> claims =null
            
        //     // Act
        //     var resLogin =  await _sut.LoginAsync(email, pwd);


        //     // // Assert
        //     Assert.NotNull(resLogin.Data);
        //     Assert.True(resLogin.Success);
        //     Assert.Equal(resLogin.Data.Token, tok.Token );
        //     Assert.Equal(resLogin.Data.RefreshToken, tok.Token );
        //     // var obj1Str = JsonConvert.SerializeObject(resGetProfile.Data);
        //     // var obj2Str = JsonConvert.SerializeObject(userDto);
        // }

           // Arrange
            
            // Act
            
            // Assert
            
       
    }
}
