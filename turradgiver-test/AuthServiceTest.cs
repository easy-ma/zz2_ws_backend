using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
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
        private readonly Mock<IRepository<User>> _customUserRepoMock = new Mock<IRepository<User>>();
        private readonly Mock<IRepository<RefreshToken>> _customRefreshTokenRepoMock = new Mock<IRepository<RefreshToken>>();
        private readonly Mock<IConfiguration> _customConfigMock = new Mock<IConfiguration>();
        
        public AuthServiceTest(){
            // IRepository<User> userRepository, IConfiguration configuration,IRepository<RefreshToken> refreshTokenRepository, ILogger<AuthService> logger
            
            var logger = new NullLogger<AuthService>();
            _sut = new AuthService(_customUserRepoMock.Object,_customConfigMock.Object, _customRefreshTokenRepoMock.Object, logger);
        }

        [Fact]
        public async Task Login_WhenUserExists()
        {
            // Arrange
            // Task<Response<AuthCredentialDto>> RegisterAsync(UserSignUpDto userSignUpDto);
            // Task<Response<AuthCredentialDto>> LoginAsync(string email, string password);
            // Task<Response<AuthCredentialDto>> RefreshToken(ExchangeRefreshTokenDto refreshDto);
            
            Guid id = new Guid("b4ae46e1-41a3-49a2-bf59-76ced244cd30"); 
            string username= "Babidiii";
            string email = "babdiii@babidiii.babidiii";
            string pwd = "Babidiii";

            var user = new User(username, email){
                Id= id,
                Password = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pwd))
            };
            var users = new List<User> (){user};
            // var userDto = new UserDto {
            //     Username = username,
            //     Email = email
            // };

            Mock<AuthService> _customAuthServiceMock = new Mock<AuthService>();
            _customUserRepoMock.Setup( x => x.GetByConditionAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users.AsQueryable());
            _customUserRepoMock.Setup( x => x.GetByConditionAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users.AsQueryable());
            _customAuthServiceMock.Setup( x => x.Authentificate(It.IsAny<User>())).ReturnsAsync(new AuthCredentialDto(){
                Token = "MyToken",
                RefreshToken = "MyRefreshToken"
            });
            // Act
            var resLogin =  await _sut.LoginAsync(email, pwd);

            // // Assert
            // Assert.NotNull(resGetProfile.Data);
            // Assert.True(resGetProfile.Success);
            // var obj1Str = JsonConvert.SerializeObject(resGetProfile.Data);
            // var obj2Str = JsonConvert.SerializeObject(userDto);
            // Assert.Equal(obj1Str, obj2Str );
        }

           // Arrange
            
            // Act
            
            // Assert
            
       
    }
}
