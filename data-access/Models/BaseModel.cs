using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.UtcNow;
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
