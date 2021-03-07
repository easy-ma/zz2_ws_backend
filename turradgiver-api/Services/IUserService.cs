using System.Threading.Tasks;
using turradgiver_api.Utils;
using turradgiver_api.Dtos.User;
using System;

namespace turradgiver_api.Services
{
    public interface IUserService
    {
        Task<Response<UserDto>> GetProfile(Guid id);
    }
}
