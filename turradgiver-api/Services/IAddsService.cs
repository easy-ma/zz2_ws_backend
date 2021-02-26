using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Utils;

namespace turradgiver_api.Services
{
    public interface IAddsService
    {
        Task<Response<IQueryable<Add>>> Filter(string text);
        Task<Response<Add>> Create(Add add);
        Task<Response<Add>> Remove(int id);
    
}
