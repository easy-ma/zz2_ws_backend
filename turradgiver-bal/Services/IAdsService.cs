using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Ads;

namespace turradgiver_bal.Services
{
    public interface IAdsService
    {
        Task<Response<AdDto>> CreateAsync(CreateAdDto createAdDto, Guid userId);
        Task<Response<object>> RemoveUserAdAsync(Guid adId, Guid userId);
        Task<Response<AdDto>> GetAdAsync(Guid id);
        Task<Response<IEnumerable<AdDto>>> GetAdsAsync(SearchDto criterias);
        Task<Response<IEnumerable<AdDto>>> GetUserAdsAsync(Guid userId, SearchDto criterias);
    }
}
