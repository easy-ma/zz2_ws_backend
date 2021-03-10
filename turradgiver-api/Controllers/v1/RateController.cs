using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;
using turradgiver_bal.Dtos.Rates;
using turradgiver_dal.Models;
using turradgiver_dal.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Utils;
using Microsoft.Extensions.Logging;
using turradgiver_bal.Services;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;



namespace turradgiver_api.Controllers.v1{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/rates")]
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
        public async Task<IActionResult> Create([FromBody] CreateRateDto createRateDto) 
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _rateService.CreateAsync(createRateDto, userId));
        }

        [HttpGet("{AdId}")]
        public async Task<IActionResult> GetAll(Guid AdId, [FromQuery] GetCommentsDto page)
        {
            return Ok(await _rateService.GetRatesAsync(AdId, page));
        }
    }   
    
}