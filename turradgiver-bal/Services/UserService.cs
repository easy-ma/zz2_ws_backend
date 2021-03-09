using System.Threading.Tasks;
using turradgiver_dal.Models;
using turradgiver_dal.Repositories;
using turradgiver_bal.Dtos;
using AutoMapper;
using turradgiver_bal.Dtos.User;
using System;

namespace turradgiver_bal.Services
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

        /// <summary>
        /// Retrieve the user profile
        /// </summary>
        /// <param name="id">UserId of the user profile to return</param>
        /// <returns>Return the UserProfileDto related to the userId provided as parameter</returns>
        public async Task<Response<UserDto>> GetProfile(Guid id)
        {
            Response<UserDto> res = new Response<UserDto>();
            User user = await _userRepository.GetByIdAsync(id);
            res.Data = _mapper.Map<UserDto>(user);
            return res;
        }
    }

}
