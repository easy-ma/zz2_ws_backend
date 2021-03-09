using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;
using turradgiver_api.Dtos.Rates;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Utils;
using Microsoft.Extensions.Logging;
using turradgiver_api.Services;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;



namespace turradgiver_api.Controllers.v1{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/comment")]
    [ApiController]
    public class RateController : ControllerBase {

        private readonly ILogger<RateController> _logger;
        private readonly IRatesService _rateService;

        public RateController(ILogger<RateController> logger, IRatesService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRateDto createRateDto) //Guid AdId
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _rateService.CreateAsync(createRateDto, userId)); //AdId
        }

        [HttpGet("{AdId}")]
        public async Task<IActionResult> GetAll(Guid AdId, [FromQuery] int page = 1)
        {
            return Ok(await _rateService.GetRatesAsync(AdId, page));
        }
    }   
    
}