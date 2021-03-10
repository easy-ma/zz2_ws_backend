#region usings
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using turradgiver_api.Utils;
using turradgiver_bal.Dtos;
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
        [SwaggerOperation(
            Summary = "Get all products",
            Description = "Get all products that meet criterias (page and search text)",
            OperationId = "GetAds"
        )]
        [SwaggerResponse(200, "All elements that met criterias", typeof(Response<IEnumerable<AdDto>>))]
        public async Task<IActionResult> GetAll([FromQuery] SearchDto criterias)
        {
            return Ok(await _adService.GetAdsAsync(criterias));
        }

        [HttpGet("{adId}")]
        [SwaggerOperation(
            Summary = "Get a specific ad",
            Description = "Get a specific ad via its id",
            OperationId = "GetAd"
        )]
        [SwaggerResponse(200, "The ad", typeof(Response<AdDto>))]
        public async Task<IActionResult> GetAd(Guid adId)
        {
            return Ok(await _adService.GetAdAsync(adId));
        }

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create an ad",
            Description = "Create an ad with the provided input",
            OperationId = "CreateAd"
        )]
        [SwaggerResponse(200, "The created ad", typeof(Response<AdDto>))]
        public async Task<IActionResult> Create([FromBody] CreateAdDto createAdDto)
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _adService.CreateAsync(createAdDto, userId));
        }

        [Authorize]
        [HttpDelete("{adId}")]
        [SwaggerOperation(
            Summary = "Remove a specific ad",
            Description = "Remove a specific ad via its id",
            OperationId = "RemoveAd"
        )]
        [SwaggerResponse(200, "It successfully deleted the ad", typeof(Response<bool>))]
        [SwaggerResponse(400, "The ad could not be deleted", typeof(Response<bool>))]
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

