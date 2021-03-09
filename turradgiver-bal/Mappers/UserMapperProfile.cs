using AutoMapper;
using turradgiver_dal.Models;
using turradgiver_bal.Dtos.User;

namespace turradgiver_bal.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
