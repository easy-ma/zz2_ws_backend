using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("RefreshToken")]
    public class RefreshToken : BaseModel
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
