using System;

namespace turradgiver_business.Dtos.Auth
{
    /// <summary>
    /// AuthCredential get from the authentification of a user
    /// </summary>
    public class AuthCredentialDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
