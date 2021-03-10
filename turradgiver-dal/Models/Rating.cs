using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace turradgiver_dal.Models
{
    [Table("Ratings")]
    class Rating
    {
        public Rating(int rate, string date)
        {
            Rate = rate;
            Date = date;
        }

        [ForeignKey("Ads")]
        public Guid AdId { get; set; }
        public virtual Ad Ad { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Column("Rate")]
        public int Rate { get; set; }

        [Column("Date")]
        public string Date { get; set; }

    }
}
