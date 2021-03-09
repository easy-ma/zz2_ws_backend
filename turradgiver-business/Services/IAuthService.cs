using System.Threading.Tasks;
using DAL.Models;
using turradgiver_business.Dtos.Auth;
using turradgiver_business.Dtos;
// using turradgiver_business.Responses.Auth;

namespace turradgiver_business.Services
{
    public interface IAuthService
    {
        Task<Response<AuthCredentialDto>> RegisterAsync(UserSignUpDto userSignUpDto);
        Task<Response<AuthCredentialDto>> LoginAsync(string email, string password);
        Task<Response<AuthCredentialDto>> RefreshToken(ExchangeRefreshTokenDto refreshDto);
    }

}
