﻿using System.Threading.Tasks;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Auth;

namespace turradgiver_bal.Services
{
    public interface IAuthService
    {
        Task<Response<AuthCredentialDto>> RegisterAsync(UserSignUpDto userSignUpDto);
        Task<Response<AuthCredentialDto>> LoginAsync(string email, string password);
        Task<Response<AuthCredentialDto>> RefreshToken(ExchangeRefreshTokenDto refreshDto);
    }

}
