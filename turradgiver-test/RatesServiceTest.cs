using System;
using System.Threading.Tasks;
using Moq;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using AutoMapper;
using Xunit;

using turradgiver_bal.Services;
using turradgiver_bal.Mappers;
using turradgiver_bal.Dtos.Rates;

using turradgiver_dal.Models;
using turradgiver_dal.Repositories;

namespace turradgiver_test
{
    public class RatesServiceTest
    {
        private readonly RatesService _sut;
        private readonly DbContextFixture _dbContext;
        private readonly IRepository<Rating> _customRateRepo;
        private readonly IRepository<Ad> _customAdRepo;
        private readonly IMapper _mapper;

        public RatesServiceTest()
        {
            // Database
            _dbContext = new DbContextFixture();
            _dbContext.Database.EnsureCreated();

            // Logger
            var logger = new NullLogger<RatesService>();

            // Mapper 
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RateMapperProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();
            
            // Repository
            _customRateRepo = new Repository<Rating>(_dbContext);
            _customAdRepo = new Repository<Ad>(_dbContext);

            _sut = new RatesService(_customRateRepo,_customAdRepo, _mapper, logger);
        }


        [Fact]
        public async Task CreateAsync () 
        {
            // Arrange
            var createDto = new CreateRateDto()
            {
                AdId = new Guid("9fb9a8d5-773f-4ca7-86c2-99ef1dd45876"),
                Title = "My title for Test",
                Comment = "My comment for test",
                Rate = 2
            };
            var userId = new Guid("ffc46d9a-4502-4454-b1bf-dd65fc2b3069");

            // Act
            var resCreate = await _sut.CreateAsync(createDto, userId);
            
            // Assert
            Assert.NotNull(resCreate.Data);
            Assert.True(resCreate.Success);
            Assert.Equal(resCreate.Data.Title, createDto.Title);
        }
    }
}
