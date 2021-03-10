#region usings
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(
            Summary = "Verify the token valididity",
            Description = "Verify whether the current user token is valid or not",
            OperationId = "VerifyToken"
        )]
        [SwaggerResponse(200, "Wether the token is valid", typeof(Response<bool>))]
        public IActionResult Verify()
        {
            Response<bool> res = new Response<bool>() { Data = true };
            return Ok(res);
        }

        [HttpPost("sign-up")]
        [SwaggerOperation(
            Summary = "Sign up to the application",
            Description = "Sign up and create an account for a user",
            OperationId = "SignUp"
        )]
        [SwaggerResponse(200, "The user successfully signed up", typeof(Response<AuthCredentialDto>))]
        [SwaggerResponse(400, "The user could not sign up", typeof(Response<AuthCredentialDto>))]
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
        [SwaggerOperation(
            Summary = "Sign in the application",
            Description = "Sign in and open a session for the user",
            OperationId = "SignIn"
        )]
        [SwaggerResponse(200, "The user successfully signed in", typeof(Response<AuthCredentialDto>))]
        [SwaggerResponse(400, "The user could not sign in", typeof(Response<AuthCredentialDto>))]
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
        [SwaggerOperation(
            Summary = "Refresh the token",
            Description = "Refresh the token and generate a new token with the RefreshToken",
            OperationId = "RefreshToken"
        )]
        [SwaggerResponse(200, "The token was successfully refreshed", typeof(Response<AuthCredentialDto>))]
        [SwaggerResponse(400, "The token could not be refreshed", typeof(Response<AuthCredentialDto>))]
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
