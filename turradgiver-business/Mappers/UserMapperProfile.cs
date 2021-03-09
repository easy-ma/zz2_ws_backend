using AutoMapper;
using DAL.Models;
using turradgiver_business.Dtos.User;

namespace turradgiver_business.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}