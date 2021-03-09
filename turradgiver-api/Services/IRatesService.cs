using System.Linq;
using System.Threading.Tasks;
using turradgiver_api.Dtos.Rates;
using DAL.Models;
using turradgiver_api.Utils;
using System;


namespace turradgiver_api.Services
{
    public interface IRatesService
    {
        Task<Response<IQueryable<Rating>>> GetRatesAsync( Guid AdId, int page);
        Task<Response<Rating>> CreateAsync(CreateRateDto createRateDto, Guid userId, Guid AdId);

    }
}
