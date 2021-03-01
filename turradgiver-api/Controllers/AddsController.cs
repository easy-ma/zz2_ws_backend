using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Dtos.Home;
using turradgiver_api.Services;



namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddsController : ControllerBase
    {
        private readonly ILogger<AddsController> _logger;
        private readonly IRepository<Add> _addsRepo;
        private readonly IAddsService _addsService;
        public AddsController(ILogger<AddsController> logger, IRepository<Add> addsRepo, IAddsService addsService)
        {
            _logger = logger;
            _addsRepo = addsRepo;
            _addsService = addsService;
        }

        [HttpPost("get-all")]
        public IActionResult GetAll([FromBody] PageDto page)
        {
            return Ok(_addsRepo.GetByRange(10 * (page.page - 1), 10));
            // return Ok(_addsRepo.GetAll());

        }
        // [Authorize]
        [HttpPost("createAdds")]
        public async Task<IActionResult> CreateAdds([FromBody] AddsDto addsDto)
        {
            _logger.LogInformation("AddsDto", addsDto);
            return Ok(await _addsService.Create(new Add(addsDto.name, addsDto.description, addsDto.price)));
        }

        // [Authorize]
        [HttpPost("removeAdd")]
        public async Task<IActionResult> RemoveAdds([FromBody] AddsIdDto id)
        {
            _logger.LogInformation("Id", id);
            return Ok(await _addsService.Remove(id.id));
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchDto searchDto)
        {
            _logger.LogInformation("SearchDto", searchDto);
            return Ok(await _addsService.Filter(searchDto.text));
        }
    }
}

