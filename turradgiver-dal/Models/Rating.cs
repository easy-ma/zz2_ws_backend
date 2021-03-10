using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace turradgiver_dal.Models
{
    [Table("Ratings")]
    public class Rating : BaseModel
    {
        [ForeignKey("Ads")]
        public Guid AdId { get; set; }
        public virtual Ad Ad { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Column("Rate")]
        public int Rate { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
