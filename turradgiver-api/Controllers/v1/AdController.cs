﻿#region usings
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using turradgiver_api.Utils;
using turradgiver_bal.Dtos.Ads;
using turradgiver_bal.Services;
#endregion 

namespace turradgiver_api.Controllers.v1
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchDto criterias)
        {
            return Ok(await _adService.GetAdsAsync(criterias));
        }

        [HttpGet("{adId}")]
        public async Task<IActionResult> GetAd(Guid adId)
        {
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
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}

