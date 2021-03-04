using System.Threading.Tasks;
using DAL.Models;
using turradgiver_api.Dtos.Auth;
using turradgiver_api.Utils;
using turradgiver_api.Responses.Auth;

namespace turradgiver_api.Services
{
    public interface IAuthService
    {
        Task<Response<AuthCredential>> RegisterAsync(User user, string password);
        Task<Response<AuthCredential>> LoginAsync(string email, string password);
        Task<Response<AuthCredential>> RefreshToken(string rToken);
    }

}
