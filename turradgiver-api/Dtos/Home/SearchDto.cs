using System.ComponentModel.DataAnnotations;

namespace turradgiver_api.Dtos.Home
{
    public class SearchDto
    {
        [Required]
        public string text { get; set; }
    }
}