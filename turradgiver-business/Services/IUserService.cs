using System.Threading.Tasks;
using turradgiver_business.Dtos;
using turradgiver_business.Dtos.User;
using System;

namespace turradgiver_business.Services
{
    public interface IUserService
    {
        Task<Response<UserDto>> GetProfile(Guid id);
    }
}
