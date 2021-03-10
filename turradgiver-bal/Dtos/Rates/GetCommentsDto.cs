using System;
using System.ComponentModel.DataAnnotations;


namespace turradgiver_bal.Dtos.Rates
{
    public class GetCommentsDto
    {

        [Range(1, float.MaxValue, ErrorMessage = "Please enter valid page Number")]
        public int page { get; set; } = 1;
    }
}