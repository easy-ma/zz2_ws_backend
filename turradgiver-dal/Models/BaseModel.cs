﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace turradgiver_dal.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.UtcNow;
        }
        
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}