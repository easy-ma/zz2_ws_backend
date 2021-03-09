using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Ads")]
    public class Ad : BaseModel
    {
        //public Ad(string name, string description, float price, string imageURL)
        //{
        //    Name = name;
        //    Description = description;
        //    Rate = 0;
        //    Price = price;
        //    ImageURL = imageURL;
        //}

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Rate")]
        public float Rate { get; set; } = 0;

        [Column("Price")]
        public float Price { get; set; }

        [Column("ImageURL")]
        public string ImageURL { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
