using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Utils;

namespace turradgiver_api.Services
{
    public interface IAdsService
    {
        Task<Response<IQueryable<Ads>>> FilterAsync(string text);
        Task<Response<Ads>> CreateAsync(Ads add);
        Task<Response<Ads>> RemoveAsync(int id);
        Task<Response<IQueryable<Ads>>> GetUserAds(int userId);
    }
}
