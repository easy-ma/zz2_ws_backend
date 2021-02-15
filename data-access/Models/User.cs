using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
  [Table("users")]
    public class User :BaseModel
    {
        public User(string username, string email) 
        {
            Username = username;
            Email = email;
        }

        [Column("username")]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}