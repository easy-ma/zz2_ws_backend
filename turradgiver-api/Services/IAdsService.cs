using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Utils;
using turradgiver_api.Dtos.Ads;

namespace turradgiver_api.Services
{
    public interface IAdsService
    {
        Task<Response<IQueryable<Ads>>> FilterAsync(string text);
        Task<Response<Ads>> CreateAsync(CreateAdDto createAdDto, int userId);
        Task<Response<Ads>> RemoveAsync(int id);
        Task<Response<Ads>> GetAdAsync(int id);
        Task<Response<IQueryable<Ads>>> GetUserAds(int userId);
    }
}
