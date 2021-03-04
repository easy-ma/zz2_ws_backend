using AutoMapper;
using turradgiver_api.Dtos.User;
using DAL.Models;

namespace turradgiver_api.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}