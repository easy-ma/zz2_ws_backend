using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Utils;

namespace turradgiver_api.Services
{
    public interface IAuthService
    {
        byte[] Register(User user, string password);
        Task<Response<string>> Login(string email, string password);

        
    }

}
