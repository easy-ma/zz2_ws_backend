using System.ComponentModel.DataAnnotations;
using System;

namespace turradgiver_bal.Dtos.Ads
{
    /// <summary>
    /// DTO for the add ID received from the frontend
    /// </summary>
    public class AdIdDto
    {
        [Required]
        public Guid Id { get; set; }

    }
}
