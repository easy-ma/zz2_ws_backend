using System;
using System.ComponentModel.DataAnnotations;


namespace turradgiver_bal.Dtos.Rates
{
    /// <summary>
    /// Dto for comment pagination
    /// </summary>
    public class PageCommentDto
    {

        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid page Number")]
        public int Page { get; set; } = 1;
    }
}
