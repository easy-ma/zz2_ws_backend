using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Utils;
using turradgiver_api.Dtos.Auth;

namespace turradgiver_api.Services
{
    public interface IAuthService
    {
        Task<Response<AuthCredential>> Register(User user, string password);
        Task<Response<AuthCredential>> Login(string email, string password);
    }

}
