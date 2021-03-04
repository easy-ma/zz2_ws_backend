using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseModel
    {

        [Column("Token")]
        public string Token { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public virtual User User { get; set; }
    }
}
