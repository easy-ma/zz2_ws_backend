using AutoMapper;
using turradgiver_api.Dtos.Rates;
using DAL.Models;
using System.Linq;

namespace turradgiver_api.Mappers
{
    public class RateMapperProfile : Profile
    {
        public RateMapperProfile()
        {
            CreateMap<CreateRateDto, Rating>();
            CreateMap<RateDto, Rating>().ReverseMap();
        }
    }
}