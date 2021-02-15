using DAL.Models;
using DAL.Repositories;
using turradgiver_api.Dtos.UserSignUpDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Dtos.UserSignInDto;
using turradgiver_api.Services;
using System.Threading.Tasks;
using turradgiver_api.Utils;

namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;

        private readonly IRepository<User> _userRepo;
        // private readonly IGenericRepository<User> _repo;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger,IAuthService authService, IRepository<User> userRepo)
        {
            _logger = logger;
            _authService = authService;
            _userRepo=userRepo;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            // _logger.LogInformation("Get all");
            // return Ok(_repo.GetAll());
            return Ok(_userRepo.GetAll());
            // return this._repo.GetTestByID("12345");
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(UserSignUpDto userSignUpDto)
        {
            _logger.LogInformation("UserSignUpDto",userSignUpDto);
            Response<string> res = await _authService.Register(new User(userSignUpDto.Username, userSignUpDto.Email), userSignUpDto.Password);
            if (! res.Success){
                return BadRequest(res.Message);
            }
            return Ok(res);
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn(UserSignInDto userSignInDto)
        {
            if (userSignInDto is null)
            {
                BadRequest();
            }

         _logger.LogInformation("Get user");
            //this._repo.Insert("12345", "alex", "alex@gmail.com");
            // _repo.Create(new User(0,"bilel", "testbindon@gmail.com","okdkea"));
            // _logger.LogInformation("User",_repo.Get(0));
            return Ok("ok");
            
            // return this._repo.GetTestByID("12345");
        }


    }
}
