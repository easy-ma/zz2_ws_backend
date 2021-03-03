using System.ComponentModel.DataAnnotations;

namespace turradgiver_api.Dtos.Ads
{
    /// <summary>
    /// 
    /// </summary>
    public class AdDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5,
        ErrorMessage = "Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(200, MinimumLength = 10,
        ErrorMessage = "Description should be minimum 10 characters and a maximum of 200 characters")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Price { get; set; }
    }
}