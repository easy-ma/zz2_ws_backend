using AutoMapper;
using turradgiver_bal.Dtos.Rates;
using turradgiver_dal.Models;
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