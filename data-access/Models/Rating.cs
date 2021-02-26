using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("Ratings")]
    class Rating
    {
        public Rating(int rate, string date)
        {
            Rate = rate;
            Date = date;
        }

        [ForeignKey("Adds")]
        public string AddId { get; set; }
        public Add Add { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }
        
        [Column("rate")]
        public int Rate { get; set; }

        [Column("date")]
        public string Date { get; set; }

    }
}
