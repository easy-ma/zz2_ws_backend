using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace turradgiver_dal.Models
{
    /// <summary>
    /// Represent an Ad
    /// </summary>
    [Table("Ads")]
    public class Ad : BaseModel
    {
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
