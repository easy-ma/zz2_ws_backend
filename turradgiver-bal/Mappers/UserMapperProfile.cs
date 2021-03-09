using AutoMapper;
using turradgiver_dal.Models;
using turradgiver_bal.Dtos.User;

namespace turradgiver_bal.Mappers
{
    /// <summary>
    /// Map Users to Dto
    /// </summary>
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
