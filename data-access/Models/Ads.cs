using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Ads")]
    public class Ad : BaseModel
    {
        public Ad(string name, string description, float price)
        {
            Name = name;
            Description = description;
            Rate = 0;
            Price = price;
        }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Rate")]
        public float Rate { get; set; }

        [Column("Price")]
        public float Price { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
