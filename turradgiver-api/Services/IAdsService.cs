using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Utils;

namespace turradgiver_api.Services
{
    public interface IAdsService
    {
        Task<Response<IQueryable<Ads>>> Filter(string text);
        Task<Response<Ads>> Create(Ads add);
        Task<Response<Ads>> Remove(int id);
    }
}
