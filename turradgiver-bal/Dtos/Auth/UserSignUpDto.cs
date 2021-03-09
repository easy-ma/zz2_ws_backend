using System.ComponentModel.DataAnnotations;

namespace turradgiver_bal.Dtos.Auth
{
    /// <summary>
    /// DTO for the user sign-up DTO
    /// </summary>
    public class UserSignUpDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
