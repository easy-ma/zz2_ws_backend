using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Dtos.Home;
using turradgiver_api.Services;

namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly ILogger<AdsController> _logger;
        private readonly IRepository<Ads> _addsRepo;
        private readonly IAdsService _addsService;
        public AdsController(ILogger<AdsController> logger, IRepository<Ads> addsRepo, IAdsService addsService)
        {
            _logger = logger;
            _addsRepo = addsRepo;
            _addsService = addsService;
        }

        [HttpGet("ads")]
        public IActionResult GetAll([FromQuery] int page = 1)
        {
            //return Ok(_addsRepo.GetByRange(10 * (page.page - 1), 10));
            // return Ok(_addsRepo.GetAll());
            Debug.WriteLine("LA");
            Debug.WriteLine(page);

            return Ok(new[] { "coucou", "c'est", "moi" });
        }

        [Authorize]
        [HttpGet("get")]
        public IActionResult GetAllForUser([FromQuery] int page = 1)
        {
            Debug.WriteLine(HttpContext.User.Identity.Name);
            //return Ok(_addsRepo.GetByRange(10 * (page.page - 1), 10));
            // return Ok(_addsRepo.GetAll());
            Debug.WriteLine("LA");
            Debug.WriteLine(page);

            return Ok(new[] { "coucou", "c'est", "moi" });
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AdDto addsDto)
        {
            _logger.LogInformation("AddsDto", addsDto);
            return Ok(await _addsService.CreateAsync(new Ads(addsDto.name, addsDto.description, addsDto.price)));
        }

        [Authorize]
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromBody] AdIdDto id)
        {
            _logger.LogInformation("Id", id);
            return Ok(await _addsService.RemoveAsync(id.id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchDto searchDto)
        {
            _logger.LogInformation("SearchDto", searchDto);
            return Ok(await _addsService.FilterAsync(searchDto.text));
        }
    }
}

