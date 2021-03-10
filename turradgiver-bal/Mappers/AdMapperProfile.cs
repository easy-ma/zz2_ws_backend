#region usings
using AutoMapper;
using turradgiver_bal.Dtos.Ads;
using turradgiver_dal.Models;

#endregion

namespace turradgiver_bal.Mappers
{
    /// <summary>
    /// Map Ads to Dto
    /// </summary>
    public class AdMapperProfile : Profile
    {
        public AdMapperProfile()
        {
            CreateMap<CreateAdDto, Ad>();
            CreateMap<AdDto, Ad>().ReverseMap();
        }
    }
}
