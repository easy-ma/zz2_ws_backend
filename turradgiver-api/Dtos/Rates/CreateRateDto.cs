using System;
using System.ComponentModel.DataAnnotations;

namespace turradgiver_api.Dtos.Rates
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRateDto
    {
        [Required]
        public Guid AdId {get;set;}

        [Required]
        [StringLength(50, MinimumLength = 5,
        ErrorMessage = "Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(200, MinimumLength = 0,
        ErrorMessage = "Comment should be minimum 10 characters and a maximum of 200 characters")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        
        [Required]
        [Range(0, 5, ErrorMessage = "Please enter valid int  Number between 0 and 5")]
        public int Rate { get; set; }
    }
}