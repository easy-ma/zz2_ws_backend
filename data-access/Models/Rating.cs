using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("Ratings")]
    public class Rating : BaseModel
    {
        public Rating(int rate, string comment, string name)
        {
            Rate = rate;
            Name = name;
            Comment = comment;
        }

        [ForeignKey("Ads")]
        public Guid AdId { get; set; }
        public virtual Ad Ad { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        
        [Column("Rate")]
        public int Rate { get; set; }
        [Column("Comment")]
        public string Comment {get;set;}

        [Column("Name")]
        public string Name {get;set;}
    }
}
