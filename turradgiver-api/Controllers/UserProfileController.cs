using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Services;
using turradgiver_api.Utils;
using System.Security.Claims;
using DAL.Repositories;
using System;
using AutoMapper;
using turradgiver_api.Dtos.User;
using turradgiver_api.Dtos.Ads;
using System.Linq;

namespace turradgiver_api.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly IRepository<User> _userRepository; 
        private readonly IAdsService _adsService;
        private readonly IMapper _mapper;

        public UserProfileController(IAdsService adsService, IMapper mapper,ILogger<UserProfileController> logger, IRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _adsService = adsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            string id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = await _userRepository.GetByIdAsync(Convert.ToInt32(id));
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("ads")]
        public async Task<IActionResult> GetAds()
        {
            string id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Response<IQueryable<Ads>> resAds = await _adsService.GetUserAds(Convert.ToInt32(id));
            if (resAds.Success){
                return Ok(resAds);
            }
            return NotFound(resAds);
        }
    }
}
