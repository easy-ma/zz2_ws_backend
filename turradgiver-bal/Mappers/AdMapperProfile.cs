#region usings
using AutoMapper;

using turradgiver_dal.Models;
using turradgiver_bal.Dtos.Ads;
#endregion

namespace turradgiver_bal.Mappers
{
    public class AdMapperProfile : Profile
    {
        public AdMapperProfile()
        {
            CreateMap<CreateAdDto, Ad>();
            CreateMap<AdDto, Ad>().ReverseMap();
        }
    }
}
