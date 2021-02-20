using System;

namespace turradgiver_api.Dtos.Auth
{
    /// <summary>
    /// AuthCredential get from the authentification of a user
    /// </summary>
    public class AuthCredential
    {
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
        // Should add refresh token
    }
}