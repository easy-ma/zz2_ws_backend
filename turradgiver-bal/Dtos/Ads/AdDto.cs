using System;

namespace turradgiver_bal.Dtos.Ads
{
    /// <summary>
    /// DTO for an Ad received or sent
    /// </summary>
    public class AdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public float Price { get; set; }
        public string ImageURL { get; set; }
    }
}
