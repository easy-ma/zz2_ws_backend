using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Utils;
using Microsoft.Extensions.Logging;
using turradgiver_api.Dtos.Ads;
using turradgiver_api.Services;
using System;
using System.Security.Claims;

namespace turradgiver_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly ILogger<AdsController> _logger;
        private readonly IAdsService _addsService;

        public AdsController(ILogger<AdsController> logger, IAdsService addsService)
        {
            _logger = logger;
            _addsService = addsService;
        }

        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] int page = 1)
        {
            return Ok(new[] { "coucou", "c'est", "moi" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAd(int id)
        {
            return Ok(await _addsService.GetAdAsync(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdDto createAdDto)
        {
            int id = HttpContext.GetUserId();
            return Ok(await _addsService.CreateAsync(createAdDto, id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            //FIXME: check if the addsBelong to the user
            return Ok(await _addsService.RemoveAsync(id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchDto searchDto)
        {
            return Ok(await _addsService.FilterAsync(searchDto.Text));
        }
    }
}

