using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace DAL.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseModel
    {

        [Column("Token")]
        public string Token { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        
        public virtual User User { get; set; }
    }
}
