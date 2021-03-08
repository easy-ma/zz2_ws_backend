using AutoMapper;
using turradgiver_api.Dtos.Ads;
using DAL.Models;
using System.Linq;
using System.Collections.Generic;

namespace turradgiver_api.Mappers
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
