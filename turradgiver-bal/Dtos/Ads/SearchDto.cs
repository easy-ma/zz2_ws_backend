using System.ComponentModel.DataAnnotations;

namespace turradgiver_bal.Dtos.Ads
{
    /// <summary>
    /// DTO for the search criteria (page and search field)
    /// </summary>
    public class SearchDto
    {
        private string _search;
        public string Search
        {
            get
            {
                return this._search;

            }
            set
            {
                this._search = (value ?? string.Empty).ToLower();
            }
        }

        [Range(1, float.MaxValue, ErrorMessage = "Please enter valid page Number")]
        public int Page { get; set; } = 1;
    }
}
