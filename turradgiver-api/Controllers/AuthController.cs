using DAL.Models;
using DAL.Repositories;
using turradgiver_api.Dtos.UserSignUpDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Dtos.UserSignInDto;

namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IGenericRepository<User> _repo;

        public AuthController(ILogger<AuthController> logger, IGenericRepository<User> repository)
        {
            _logger = logger;
            _repo = repository;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Get all");
            return Ok(_repo.GetAll());
            // return this._repo.GetTestByID("12345");
        }

        [HttpPost("sign-up")]
        public IActionResult SignUp(UserSignUpDto userSignUpDto)
        {
            if (userSignUpDto is null)
            {
                BadRequest();
            }

         _logger.LogInformation("Get user");
            _logger.LogInformation("UserSignUpDto",userSignUpDto);
            _repo.Create(new User(0,userSignUpDto.Username, userSignUpDto.Email,userSignUpDto.Password));
            return Ok("ok");
            // return this._repo.GetTestByID("12345");
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
            _logger.LogInformation("User",_repo.Get(0));
            return Ok("ok");
            // return this._repo.GetTestByID("12345");
        }


    }
}
