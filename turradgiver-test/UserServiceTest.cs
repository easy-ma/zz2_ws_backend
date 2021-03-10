using System.Threading.Tasks;
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

namespace turradgiver_test
{
    public class UserServiceTest
    {
        private readonly UserService _sut;
        private readonly Mock<IRepository<User>> _customUserRepoMock = new Mock<IRepository<User>>();
        
        public UserServiceTest(){
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new UserMapperProfile()); });
            var mapper = mapperConfiguration.CreateMapper();
            
            _sut = new UserService(_customUserRepoMock.Object, mapper);
        }

        [Fact]
        public async Task GetProfile_WhenUserExist()
        {
            // Arrange
            Guid id = new Guid("b4ae46e1-41a3-49a2-bf59-76ced244cd30"); 
            string username= "Babidiii";
            string email = "babdiii@babidiii.babidiii";
            
            var user = new User(username, email){
                Id= id,
            };
            var userDto = new UserDto {
                Username = username,
                Email = email
            };

            _customUserRepoMock.Setup( x => x.GetByIdAsync(id)).ReturnsAsync(user);

            // Act
            var resGetProfile =  await _sut.GetProfile(id);

            // Assert
            Assert.NotNull(resGetProfile.Data);
            Assert.True(resGetProfile.Success);
            var obj1Str = JsonConvert.SerializeObject(resGetProfile.Data);
            var obj2Str = JsonConvert.SerializeObject(userDto);
            Assert.Equal(obj1Str, obj2Str );
        }
        
        [Fact]
        public async Task GetProfile_WhenUserDoesNotExist()
        {
             // Arrange
            Guid id = new Guid("b4ae46e1-41a3-49a2-bf59-76ced244cd30"); 
            string username= "Babidiii";
            string email = "babdiii@babidiii.babidiii";
            
            var user = new User(username, email){
                Id= id,
            };
            var userDto = new UserDto {
                Username = username,
                Email = email
            };

            _customUserRepoMock.Setup( x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(()=>null);

            // Act
            var resGetProfile =  await _sut.GetProfile(id);

            // Assert
            Assert.True(resGetProfile.Success);
            Assert.Null(resGetProfile.Data);
        }
    }
}
