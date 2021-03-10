using System.ComponentModel.DataAnnotations.Schema;

namespace turradgiver_dal.Models
{
    /// <summary>
    /// Represent a User
    /// </summary>
    [Table("Users")]
    public class User : BaseModel
    {
        [Column("username")]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public byte[] Password { get; set; }
    }
}
