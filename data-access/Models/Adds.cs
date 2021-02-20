using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
  [Table("Adds")]
    public class Adds :BaseModel
    {
        public Adds(string name, string description) 
        {
            Name = name;
            Description = description;
        }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("rate")]
        public int Rate { get; set; }
    }
}