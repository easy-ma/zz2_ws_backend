using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Dtos.Home;
using turradgiver_api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using turradgiver_api.Utils;



namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Adds> _addsRepo;
        private readonly IHomeService _homeService;
        public HomeController(ILogger<HomeController> logger, IRepository<Adds> addsRepo, IHomeService homeService)
        {
            _logger = logger;
            _addsRepo = addsRepo;
            _homeService = homeService;
        }

        [Authorize]
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            // _logger.LogInformation("Object",o);
            return Ok(_addsRepo.GetAll());
        }

        [HttpPost("{search}")]
        // [ProducesResponseType(StatusCodes.Status200Ok, Type = typeof(Adds))]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromBody] SearchDto searchDto)
        {
            _logger.LogInformation("SearchDto",searchDto);
            Response<AddsDto> res = await _homeService.Filter(searchDto.text);
            
            
            //if (! res.Success){
            //    return Unauthorized(res.Message);
            //}

            return Ok(res);
        }
    }
}
