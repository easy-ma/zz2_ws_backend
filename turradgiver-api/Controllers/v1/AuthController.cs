#region usings
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Auth;
using turradgiver_bal.Services;
#endregion

namespace turradgiver_api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/auth")]
    [ApiController]
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
            Response<AuthCredentialDto> res = await _authService.RegisterAsync(userSignUpDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInDto userSignInDto)
        {
            Response<AuthCredentialDto> res = await _authService.LoginAsync(userSignInDto.Email, userSignInDto.Password);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] ExchangeRefreshTokenDto exRefreshTokenDto)
        {
            Response<AuthCredentialDto> res = await _authService.RefreshToken(exRefreshTokenDto);
            if (!res.Success)
            {
                return BadRequest(res); 
            }
            return Ok(res);
        }
    }
}
