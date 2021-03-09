using AutoMapper;
using turradgiver_bal.Dtos.User;
using turradgiver_dal.Models;

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
