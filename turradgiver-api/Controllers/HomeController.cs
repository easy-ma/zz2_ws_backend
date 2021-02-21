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
        private readonly IRepository<Add> _addsRepo;
        private readonly IAddsService _addsService;
        public HomeController(ILogger<HomeController> logger, IRepository<Add> addsRepo, IAddsService addsService)
        {
            _logger = logger;
            _addsRepo = addsRepo;
            _addsService = addsService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_addsRepo.GetAll());
        }
        // [Authorize]
        [HttpPost("createAdds")]
         public async Task<IActionResult> CreateAdds([FromBody] AddsDto addsDto)
        {
            _logger.LogInformation("AddsDto",addsDto);   
            return Ok(await _addsService.Create(new Add(addsDto.name, addsDto.description, addsDto.price)));
        }

        // [Authorize]
        [HttpPost("removeAdd")]
         public async Task<IActionResult> RemoveAdds([FromBody] Add add)
        {
            _logger.LogInformation("Add",add);   
            return Ok(await _addsService.Remove(add));
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchDto searchDto)
        {
            _logger.LogInformation("SearchDto",searchDto);            
            return Ok(await _addsService.Filter(searchDto.text));
        }
    }
}

