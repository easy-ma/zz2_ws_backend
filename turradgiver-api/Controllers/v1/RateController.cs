using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using turradgiver_api.Utils;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Rates;
using turradgiver_bal.Services;



namespace turradgiver_api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/rates")]
    [ApiController]
    public class RateController : ControllerBase
    {

        private readonly ILogger<RateController> _logger;
        private readonly IRatesService _rateService;

        public RateController(ILogger<RateController> logger, IRatesService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a rating",
            Description = "Create a rating with the privided input",
            OperationId = "CreateRate"
        )]
        [SwaggerResponse(200, "The created rating", typeof(Response<RateDto>))]
        public async Task<IActionResult> Create([FromBody] CreateRateDto createRateDto)
        {
            Guid userId = HttpContext.GetUserId();
            return Ok(await _rateService.CreateAsync(createRateDto, userId));
        }

        [HttpGet("{AdId}")]
        [SwaggerOperation(
            Summary = "Get all rates for an ad",
            Description = "Get all ratings for the scecified ad",
            OperationId = "GetRates"
        )]
        [SwaggerResponse(200, "The rates for this ad", typeof(Response<IEnumerable<RateDto>>))]
        public async Task<IActionResult> GetAll(Guid AdId, [FromQuery(Name="Page")] int page)
        {
            return Ok(await _rateService.GetRatesAsync(AdId, new GetCommentsDto() { Page = page }));
        }
    }

}
