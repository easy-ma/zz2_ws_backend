using System.ComponentModel.DataAnnotations;

namespace turradgiver_business.Dtos.Ads
{
    public class SearchDto
    {
        public string Search { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Please enter valid page Number")]
        public int Page { get; set; } = 1;
    }
}
