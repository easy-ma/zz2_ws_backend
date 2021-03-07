using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using turradgiver_api.Utils;
using AutoMapper;
using turradgiver_api.Dtos.User;
using System;

namespace turradgiver_api.Services
{
    /// <summary>
    /// Class <c>HomeService</c> provide adds according to the user input
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> GetProfile(Guid id)
        {
            Response<UserDto> res = new Response<UserDto>();
            User user = await _userRepository.GetByIdAsync(id);
            res.Data = _mapper.Map<UserDto>(user);
            return res;
        }
    }

}
