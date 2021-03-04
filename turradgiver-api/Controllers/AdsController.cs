using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Dtos.Ads;
using turradgiver_api.Services;
using AutoMapper;
using System;
using System.Security.Claims;

namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly ILogger<AdsController> _logger;
        private readonly IRepository<Ads> _addsRepo;
        private readonly IAdsService _addsService;
        private readonly IMapper _mapper;

        public AdsController(IMapper mapper, ILogger<AdsController> logger, IRepository<Ads> addsRepo, IAdsService addsService)
        {
            _logger = logger;
            _addsRepo = addsRepo;
            _addsService = addsService;
            _mapper = mapper;
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
        public async Task<IActionResult> Create([FromBody] CreateAdDto createAdDto)
        {
            string id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Ads ads = _mapper.Map<Ads>(createAdDto);
            ads.UserId = Convert.ToInt32(id);
            return Ok(await _addsService.CreateAsync(ads));
        }

        [Authorize]
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromBody] AdIdDto idDto)
        {
            _logger.LogInformation("Id", idDto.Id);
            return Ok(await _addsService.RemoveAsync(idDto.Id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchDto searchDto)
        {
            _logger.LogInformation("SearchDto", searchDto);
            return Ok(await _addsService.FilterAsync(searchDto.Text));
        }
    }
}

