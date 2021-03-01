using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Dtos.Auth;
using turradgiver_api.Utils;

namespace turradgiver_api.Services
{
    public interface IAuthService
    {
        Task<Response<AuthCredential>> Register(User user, string password);
        Task<Response<AuthCredential>> Login(string email, string password);
    }

}
