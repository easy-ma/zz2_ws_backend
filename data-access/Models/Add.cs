using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("Adds")]
    class Add
    {
        public Add(string id )
        {
            Id = id;
        }

        [Key]
        [Column("id")]
        public string Id { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }

    }
}
