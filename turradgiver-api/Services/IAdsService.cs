using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Utils;
using turradgiver_api.Dtos.Ads;
using System;

namespace turradgiver_api.Services
{
    public interface IAdsService
    {
        Task<Response<IQueryable<Ad>>> FilterAsync(string text);
        Task<Response<Ad>> CreateAsync(CreateAdDto createAdDto, Guid userId);
        Task<Response<Ad>> RemoveAsync(Guid id);
        Task<Response<Ad>> GetAdAsync(Guid id);
        Task<Response<IQueryable<Ad>>> GetUserAds(Guid userId);
        Task<bool> CheckIfAdExistAndBelongToUserAsync(Guid adId, Guid userId);
    }
}
