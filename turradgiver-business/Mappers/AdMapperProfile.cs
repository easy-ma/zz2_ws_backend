#region usings
using AutoMapper;

using DAL.Models;
using turradgiver_business.Dtos.Ads;
#endregion

namespace turradgiver_business.Mappers
{
    public class AdsMapperProfile : Profile
    {
        public AdsMapperProfile()
        {
            CreateMap<CreateAdDto, Ad>();
            CreateMap<AdDto, Ad>().ReverseMap();
        }
    }
}
