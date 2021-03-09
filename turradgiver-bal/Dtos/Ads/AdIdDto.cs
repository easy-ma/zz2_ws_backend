using System.ComponentModel.DataAnnotations;
using System;

namespace turradgiver_bal.Dtos.Ads
{
    /// <summary>
    /// 
    /// </summary>
    public class AdIdDto
    {
        [Required]
        public Guid Id { get; set; }

    }
}
