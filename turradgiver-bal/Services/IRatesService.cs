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
        Task<Response<IEnumerable<RateDto>>> GetRatesbyAdId(Guid AdId);
        public Task<bool> HandleRate(Guid AdId, int newRate);
        Task<Response<IEnumerable<RateDto>>> GetRatesAsync( Guid AdId, GetCommentsDto page);
        Task<Response<RateDto>> CreateAsync(CreateRateDto createRateDto, Guid userId);

    }
}