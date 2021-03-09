using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Dtos.Ads;
using System;

using turradgiver_business.Dtos;

namespace turradgiver_api.Services
{
    public interface IAdsService
    {
        Task<Response<IQueryable<Ad>>> FilterAsync(string text);
        Task<Response<Ad>> CreateAsync(CreateAdDto createAdDto, Guid userId);
        Task<Response<Ad>> RemoveUserAdAsync(Guid adId,Guid userId);
        Task<Response<Ad>> GetAdAsync(Guid id);
        Task<Response<IQueryable<Ad>>> GetUserAds(Guid userId);
    }
}
