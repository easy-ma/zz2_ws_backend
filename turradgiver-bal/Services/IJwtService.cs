using turradgiver_bal.Dtos.Auth;
using System.Security.Claims;
using System.Collections.Generic;

namespace turradgiver_bal.Services
{
    public interface IJwtService
    {
        TokenDto GenerateToken(byte[] secretKey,double expMinutes,IEnumerable<Claim> claims =null);
        bool ValidateToken(string refreshToken, byte[] secretKey);
    }

}
