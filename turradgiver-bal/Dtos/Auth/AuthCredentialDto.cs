
namespace turradgiver_bal.Dtos.Auth
{
    /// <summary>
    /// DTO for AuthCredential sent back to the user after auth
    /// </summary>
    public class AuthCredentialDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
