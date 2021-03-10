using AutoMapper;
using turradgiver_bal.Dtos.Rates;
using turradgiver_dal.Models;

namespace turradgiver_api.Mappers
{
    public class RateMapperProfile : Profile
    {
        public RateMapperProfile()
        {
            CreateMap<CreateRateDto, Rating>();
            CreateMap<RateDto, Rating>().ForPath(
                dest => dest.User.Username,
                opt => opt.MapFrom(src => src.UserName)
            ).ReverseMap();
            CreateMap<Rating, RateDto>().ForPath(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.Username)
            );
            CreateMap<RateDto, Rating>().ReverseMap();
        }
    }
}
