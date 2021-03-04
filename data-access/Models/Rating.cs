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

        [ForeignKey("AdsId")]
        public string AdsId { get; set; }
        public Ads Ads { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        
        [Column("Rate")]
        public int Rate { get; set; }

        [Column("Date")]
        public string Date { get; set; }

    }
}
