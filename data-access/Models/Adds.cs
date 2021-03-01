using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Adds")]
    public class Add : BaseModel
    {
        public Add(string name, string description, float price)
        {
            Name = name;
            Description = description;
            Rate = 0;
            Price = price;
        }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("rate")]
        public float Rate { get; set; }

        [Column("price")]
        public float Price { get; set; }

    }
}
