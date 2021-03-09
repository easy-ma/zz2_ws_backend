using System.ComponentModel.DataAnnotations;

namespace turradgiver_bal.Dtos.Auth
{
    /// <summary>
    /// DTO for the user sign-in data
    /// </summary>
    public class UserSignInDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
