#region usings
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using turradgiver_api.Utils;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Ads;
using turradgiver_bal.Dtos.User;
using turradgiver_bal.Services;
#endregion

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
        [SwaggerOperation(
            Summary = "Get a user profile",
            Description = "Get the current user profile",
            OperationId = "UserProfile"
        )]
        [SwaggerResponse(200, "The user profile", typeof(Response<UserDto>))]
        public async Task<IActionResult> GetProfile()
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _userService.GetProfile(userId));
        }

        [HttpGet("ads")]
        [SwaggerOperation(
            Summary = "Get all ads for a user",
            Description = "Get all ads that match criterias for a user",
            OperationId = "UserAds"
        )]
        [SwaggerResponse(200, "The user ads that met provided criterias", typeof(Response<IEnumerable<AdDto>>))]
        public async Task<IActionResult> GetAds([FromQuery] SearchDto criterias)
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _adsService.GetUserAdsAsync(userId, criterias));
        }
    }
}
