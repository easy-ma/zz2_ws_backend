using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Services;
using turradgiver_api.Utils;
using System;
using turradgiver_api.Dtos.User;
using turradgiver_api.Dtos.Ads;
using System.Linq;

namespace turradgiver_api.Controllers.v1
{
    
    [Authorize]
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAdsService _adsService;
        private readonly IUserService _userService;

        public UserController(IAdsService adsService, IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _adsService = adsService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _userService.GetProfile(userId));
        }

        [HttpGet("ads")]
        public async Task<IActionResult> GetAds()
        {
            Guid userId = HttpContext.GetUserId();
            Response<IQueryable<Ad>> resAds = await _adsService.GetUserAds(userId);
            return Ok(resAds);
            
        }
    }
}