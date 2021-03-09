using System.Threading.Tasks;
using System.Linq;
using System;
using DAL.Models;

using turradgiver_business.Dtos;
using turradgiver_business.Dtos.Ads;

namespace turradgiver_business.Services
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
