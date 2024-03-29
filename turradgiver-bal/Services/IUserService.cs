﻿using System;
using System.Threading.Tasks;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.User;

namespace turradgiver_bal.Services
{
    public interface IUserService
    {
        Task<Response<UserDto>> GetProfile(Guid id);
    }
}
