using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace turradgiver_dal.Models
{
    /// <summary>
    /// Represent the refresh token
    /// </summary>
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
