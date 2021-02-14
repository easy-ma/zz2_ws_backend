using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
  [Table("tests")]
    public class Test 
    {
        public Test(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        [Column("id")]
        public string Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }
}