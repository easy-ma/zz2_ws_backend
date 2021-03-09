using System.ComponentModel.DataAnnotations;

namespace turradgiver_bal.Dtos.Ads
{
    /// <summary>
    /// DTO for the Ad received upon ad creation
    /// </summary>
    public class CreateAdDto
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

        [Required]
        [RegularExpression(@"^(https?:)?//?[^\'"" <>]+?\.(jpg|jpeg|gif|png)$", ErrorMessage = "Image URL is not valid.")]
        public string ImageURL { get; set; }
    }
}
