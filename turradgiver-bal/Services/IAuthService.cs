﻿using System.Threading.Tasks;
using turradgiver_bal.Dtos.Auth;
using turradgiver_bal.Dtos;
using turradgiver_dal.Models;

namespace turradgiver_bal.Services
{
    public interface IAuthService
    {
        Task<Response<AuthCredentialDto>> RegisterAsync(UserSignUpDto userSignUpDto);
        Task<Response<AuthCredentialDto>> LoginAsync(string email, string password);
        Task<Response<AuthCredentialDto>> RefreshToken(ExchangeRefreshTokenDto refreshDto);
        Task<AuthCredentialDto> Authenticate(User user);
    }

}
