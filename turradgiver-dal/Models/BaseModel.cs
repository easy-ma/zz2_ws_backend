using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace turradgiver_dal.Models
{
    /// <summary>
    /// Base for all database models
    /// </summary>
    public class BaseModel
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
