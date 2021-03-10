using System.Collections.Generic;
using System.Security.Claims;
using turradgiver_bal.Dtos.Auth;

namespace turradgiver_bal.Services
{
    public interface IJwtService
    {
        TokenDto GenerateToken(byte[] secretKey, double expMinutes, IEnumerable<Claim> claims = null);
        bool ValidateToken(string refreshToken, byte[] secretKey);
    }

}
