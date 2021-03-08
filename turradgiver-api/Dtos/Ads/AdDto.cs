using System.ComponentModel.DataAnnotations;
using System;

namespace turradgiver_api.Dtos.Ads
{
    /// <summary>
    /// 
    /// </summary>
    public class AdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public float Price { get; set; }
        public string ImageURL{ get; set; }
    }
}
