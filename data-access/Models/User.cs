using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
  [Table("users")]
    public class User 
    {
        public User(int id, string username, string email, string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Password= password;
            CreatedDate = DateTime.Now;
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }


        [Column("password")]
        public string Password { get; set; }


        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}