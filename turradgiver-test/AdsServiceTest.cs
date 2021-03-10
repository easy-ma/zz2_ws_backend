using System;
using System.Threading.Tasks;
using Moq;
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
            Assert.Equal(resCreate.Data.Rate, 0 );
        }

        [Fact]
        public async Task GetAdAsync_WhenTheAdExist(){
            // Arrange
            var adId = new Guid("9fb9a8d5-773f-4ca7-86c2-99ef1dd45876");

            // Act
            var resGet = await _sut.GetAdAsync(adId);

            // Assert
            Assert.NotNull(resGet.Data);
            Assert.True(resGet.Success);
            Assert.Equal(resGet.Data.Name,"My Ad");
        }


        [Fact]
        public async Task GetAdAsync_WhenTheAdDoesNotExist(){
            // Arrange
            var adId = new Guid("11111111-773f-4ca7-86c2-99ef1dd45876");

            // Act
            var resGet = await _sut.GetAdAsync(adId);

            // Assert
            Assert.Null(resGet.Data);
            Assert.False(resGet.Success);
            Assert.Equal(resGet.Message,"Ad not found.");
        }
    }
}
