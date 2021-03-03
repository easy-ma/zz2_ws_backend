using System.ComponentModel.DataAnnotations;

namespace turradgiver_api.Dtos.Auth
{
    public class ExchangeRefreshTokenDto 
    {
        [Required]
        [DataType(DataType.Text)]
        public string RefreshToken { get; set; }
    }
}