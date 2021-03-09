using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Rates;
using turradgiver_dal.Models;


namespace turradgiver_bal.Services
{
    public interface IRatesService
    {
        Task<Response<IEnumerable<RateDto>>> GetRatesAsync( Guid AdId, int page);
        Task<Response<Rating>> CreateAsync(CreateRateDto createRateDto, Guid userId); //Guid AdId)

    }
}