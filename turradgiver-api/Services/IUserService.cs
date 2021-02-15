using System;
using DAL.Models;

namespace turradgiver_api.Services.Auth
{
    public interface IUserService
    {
      User GetUser(int id);
      User GetUserByEmail(string email);
      Boolean CreateUser(string email);
      Boolean DeleteUser(string email);
      
    }

}
