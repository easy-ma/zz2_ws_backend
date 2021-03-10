using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Rates;

namespace turradgiver_bal.Services
{
    public interface IRatesService
    {
        Task<Response<IEnumerable<RateDto>>> GetRatesbyAdIdAsync(Guid AdId);
        public Task<bool> HandleRateAsync(Guid AdId, int newRate);
        Task<Response<IEnumerable<RateDto>>> GetRatesAsync(Guid AdId, GetCommentsDto page);
        Task<Response<RateDto>> CreateAsync(CreateRateDto createRateDto, Guid userId);

    }
}
