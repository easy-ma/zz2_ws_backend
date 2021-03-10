using System;
using System.Threading.Tasks;
using Moq;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using AutoMapper;
using Xunit;

using turradgiver_bal.Services;
using turradgiver_bal.Mappers;
using turradgiver_bal.Dtos.Ads;

using turradgiver_dal.Models;
using turradgiver_dal.Repositories;

namespace turradgiver_test
{
    public class AdsServiceTest
    {
        private readonly AdsService _sut;
        private readonly DbContextFixture _dbContext;
        private readonly IRepository<Ad> _customUserRepo;
        private readonly IMapper _mapper;

        public AdsServiceTest()
        {
            // Database
            _dbContext = new DbContextFixture();
            _dbContext.Database.EnsureCreated();

            // Logger
            var logger = new NullLogger<AdsService>();

            // Mapper 
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AdMapperProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();
            
            // Repository
            _customUserRepo = new Repository<Ad>(_dbContext);

            _sut = new AdsService(_customUserRepo, _mapper, logger);
        }


        [Fact]
        public async Task CreateAsync () 
        {
            // Arrange
            var createAdDto = new CreateAdDto(){
                Name = "My Ad",
                Description = "My Description",
                Location = "My Location",
                Price = 5,
                ImageURL = "My Url"
            };

            Guid id = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069");

            // Act
            var resCreate = await _sut.CreateAsync(createAdDto, id);
            
            // Assert
            Assert.NotNull(resCreate.Data);
            Assert.True(resCreate.Success);
            Assert.Equal(resCreate.Data.Name, createAdDto.Name);
            Assert.Equal(resCreate.Data.Description, createAdDto.Description);
        }

        [Fact]
        public async Task GetAdAsync_WhenTheAdExist(){
            // Arrange
            var adId = new Guid("9fb9a8d5-773f-4ca7-86c2-99ef1dd45876");
            var expectedName = "My Ad";
            // Act
            var resGet = await _sut.GetAdAsync(adId);

            // Assert
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Name,expectedName);
        }


        [Fact]
        public async Task GetAdAsync_WhenTheAdDoesNotExist(){
            // Arrange
            var adId = new Guid("11111111-773f-4ca7-86c2-99ef1dd45876");
            var expectedMessage = "Ad not found.";

            // Act
            var resGet = await _sut.GetAdAsync(adId);

            // Assert
            Assert.Null(resGet.Data);
            Assert.False(resGet.Success);
            Assert.Equal(resGet.Message,expectedMessage);
        }


        [Fact]
        public async Task RemoveUserAd_WhenAdExist_AndBelongToUser(){
            // Arrange
            var adId = new Guid("9fb9a8d5-773f-4ca7-86c2-99ef1dd45876");
            var userId = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069");
            var expectedMessage = "Remove succeed";
            
            // Act
            var resRem = await _sut.RemoveUserAdAsync(adId, userId);

            // Assert
            Assert.Null(resRem.Data);
            Assert.True(resRem.Success);
            Assert.Equal(resRem.Message, expectedMessage);
        }

        [Fact]
        public async Task RemoveUserAd_WhenAdExist_But_NotBelongToUser(){
            // Arrange
            var adId = new Guid("9fb9a8d5-773f-4ca7-86c2-99ef1dd45876");
            var userId = new Guid("11111111-4502-4454-b1bf-dd65fc2b3069");
            var expectedMessage = "Ad not found for this user.";
        
            // Act
            var resRem = await _sut.RemoveUserAdAsync(adId, userId);

            // Assert
            Assert.Null(resRem.Data);
            Assert.False(resRem.Success);
            Assert.Equal(resRem.Message, expectedMessage);
        }

        [Fact]
        public async Task RemoveUserAd_WhenAdDoesNotExist_But_BelongToUser(){
            // Arrange
            var adId = new Guid("11111111-773f-4ca7-86c2-99ef1dd45876");
            var userId = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069");
            var expectedMessage = "Ad not found for this user.";

            // Act
            var resRem = await _sut.RemoveUserAdAsync(adId, userId);

            // Assert
            Assert.Null(resRem.Data);
            Assert.False(resRem.Success);
            Assert.Equal(resRem.Message,expectedMessage);
        }

        [Fact]
        public async Task RemoveUserAd_WhenAdDoesNotExist_And_DoesNotBelongToUser(){
            // Arrange
            var adId = new Guid("11111111-773f-4ca7-86c2-99ef1dd45876");
            var userId = new Guid("11111111-4502-4454-b1bf-dd65fc2b3069");
            var expectedMessage = "Ad not found for this user.";
            
            // Act
            var resRem = await _sut.RemoveUserAdAsync(adId, userId);

            // Assert
            Assert.Null(resRem.Data);
            Assert.False(resRem.Success);
            Assert.Equal(resRem.Message,expectedMessage);
        }

        [Fact]
        public async Task GetAds_WithSearchCriteria(){
            // Arrange
            var searchDto = new SearchDto() {
                Search = "Different"
            };
            var expectedNumber = 1;

            // Act
            var resGet = await _sut.GetAdsAsync(searchDto);

            // Asser
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Count(), expectedNumber);
        }

        [Fact]
        public async Task GetAds_WithoutSearchCriteria(){
            // Arrange
            var searchDto = new SearchDto();
            var expectedNumber = 3;

            // Act
            var resGet = await _sut.GetAdsAsync(searchDto);

            // Asser
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Count(), expectedNumber);
        }

        [Fact]
        public async Task GetAds_WithPage_Outbound(){
            // Arrange
            var searchDto = new SearchDto() 
            {
                Page= 4
            };
            var expectedNumber = 0;

            // Act
            var resGet = await _sut.GetAdsAsync(searchDto);

            // Asser
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Count(), expectedNumber);
        }

        [Fact]
        public async Task GetUserAds_WithSearchCriteria(){
            // Arrange
            var searchDto = new SearchDto() {
                Search = "Description"
            };
            var userId = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069");
            var expectedNumber = 1;

            // Act
            var resGet = await _sut.GetUserAdsAsync(userId, searchDto);

            // Asser
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Count(), expectedNumber);
        }

        [Fact]
        public async Task GetUserAds_WithoutSearchCriteria(){
            // Arrange
            var searchDto = new SearchDto();
            var userId = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069");
            var expectedNumber = 2;

            // Act
            var resGet = await _sut.GetUserAdsAsync(userId, searchDto);

            // Asser
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Count(), expectedNumber);
        }

        [Fact]
        public async Task GetUserAds_WithPage_Outbound(){
            // Arrange
            var searchDto = new SearchDto() 
            {
                Page= 4
            };
            var userId = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069");
            var expectedNumber = 0;

            // Act
            var resGet = await _sut.GetUserAdsAsync(userId, searchDto);

            // Asser
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Count(), expectedNumber);
        }
    }
}
