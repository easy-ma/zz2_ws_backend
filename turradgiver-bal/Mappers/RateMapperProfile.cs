using AutoMapper;
using turradgiver_bal.Dtos.Rates;
using turradgiver_dal.Models;

namespace turradgiver_bal.Mappers
{
    public class RateMapperProfile : Profile
    {
        public RateMapperProfile()
        {
            CreateMap<CreateRateDto, Rating>();
            CreateMap<User, RateDto>();
            
            //CreateMap<Rating, RateDto>().ForMember(
            //    dest => dest.Username,
            //    opt => opt.MapFrom(src => src.User.Username)
            //);
            CreateMap<RateDto, Rating>().ReverseMap();
        }
    }
}
