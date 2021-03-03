using System.ComponentModel.DataAnnotations;

namespace turradgiver_api.Dtos.Ads
{
    /// <summary>
    /// 
    /// </summary>
    public class AdIdDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Id { get; set; }

    }
}
