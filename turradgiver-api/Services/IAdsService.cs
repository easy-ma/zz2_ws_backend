using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Utils;
using turradgiver_api.Dtos.Ads;

namespace turradgiver_api.Services
{
    public interface IAdsService
    {
        Task<Response<IQueryable<Ad>>> FilterAsync(string text);
        Task<Response<Ad>> CreateAsync(CreateAdDto createAdDto, int userId);
        Task<Response<Ad>> RemoveAsync(int id);
        Task<Response<Ad>> GetAdAsync(int id);
        Task<Response<IQueryable<Ad>>> GetUserAds(int userId);
        Task<bool> CheckIfAdBelongToUserAsync(int adId, int userId);
    }
}
