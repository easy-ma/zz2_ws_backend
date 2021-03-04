using AutoMapper;
using turradgiver_api.Dtos.User;
using DAL.Models;

namespace mynamespace
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}