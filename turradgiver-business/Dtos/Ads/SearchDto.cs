using System.ComponentModel.DataAnnotations;

namespace turradgiver_business.Dtos.Ads
{
    public class SearchDto
    {
        [Required]
        public string Text { get; set; }
    }
}
