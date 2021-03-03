using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Responses.Auth;
using turradgiver_api.Dtos.Auth;
using turradgiver_api.Services;
using turradgiver_api.Utils;

namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [Authorize]
        [HttpGet("verify")]
        public IActionResult Verify()
        {
            Response<bool> res = new Response<bool>() { Data = true };
            return Ok(res);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDto userSignUpDto)
        {
            _logger.LogInformation("UserSignUpDto", userSignUpDto);
            Response<AuthCredential> res = await _authService.RegisterAsync(new User(userSignUpDto.Username, userSignUpDto.Email), userSignUpDto.Password);
            if (!res.Success)
            {
                return Unauthorized(res);
            }
            return Ok(res);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInDto userSignInDto)
        {
            Response<AuthCredential> res = await _authService.LoginAsync(userSignInDto.Email, userSignInDto.Password);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
