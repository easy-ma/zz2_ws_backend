using System.ComponentModel.DataAnnotations.Schema;

namespace turradgiver_dal.Models
{
    [Table("Users")]
    public class User : BaseModel
    {
        public User(string username, string email) : base()
        {
            Username = username;
            Email = email;
        }

        [Column("username")]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public byte[] Password { get; set; }
    }
}
