#region usings
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using turradgiver_bal.Dtos.Auth;
#endregion

namespace turradgiver_bal.Services
{
    /// <summary>
    /// Class <c>JwtService</c> provide authentification service for the API
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate the token from claims provided, with a specific ammount of time for expirtation provided as parameter
        /// And with a secretKey received as parameter
        /// </summary>
        /// <param name="secretKey">The secretKey used for the creation of the singninCredentials</param>
        /// <param name="expMinutes">Number of minutes before the expiration of the token generated</param>
        /// <param name="claims">The claims to encode in the token, null if not provided</param>
        /// <returns>Return the string representation of the token</returns>
        public TokenDto GenerateToken(byte[] secretKey, double expMinutes, IEnumerable<Claim> claims = null)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(secretKey);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(expMinutes),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenDto()
            {
                Token = tokenHandler.WriteToken(tokenHandler.CreateToken(token)),
                Expires = (DateTime)token.Expires
            };
        }

        /// <summary>
        /// Validate the refreshToken 
        /// </summary>
        /// <param name="refreshToken">RefreshToken to validate</param>
        /// <param name="secretKey">The secretKey used for th validation</param>
        /// <returns>Return true if the RefreshToken is validate, false if not</returns>
        public bool ValidateToken(string refreshToken, byte[] secretKey)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
