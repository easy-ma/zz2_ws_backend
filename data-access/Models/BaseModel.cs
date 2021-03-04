using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.UtcNow;
        }
        
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
