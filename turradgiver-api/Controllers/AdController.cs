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
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/ads")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly ILogger<AdController> _logger;
        private readonly IAdsService _adService;

        public AdController(ILogger<AdController> logger, IAdsService adService)
        {
            _logger = logger;
            _adService = adService;
        }

        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] int page = 1)
        {
            return Ok(new[] { "coucou", "c'est", "moi" });
        }

        [HttpGet("{adId}")]
        public async Task<IActionResult> GetAd(Guid adId)
        {
            _logger.LogInformation(adId.ToString());
            return Ok(await _adService.GetAdAsync(adId));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdDto createAdDto)
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _adService.CreateAsync(createAdDto, userId));
        }

        [Authorize]
        [HttpDelete("{adId}")]
        public async Task<IActionResult> Remove(Guid adId)
        {
            Guid userId = HttpContext.GetUserId();
            var res = await _adService.RemoveUserAdAsync(adId, userId);
            if (res.Success) {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchDto searchDto)
        {
            return Ok(await _adService.FilterAsync(searchDto.Text));
        }
    }
}

