using System.ComponentModel.DataAnnotations;

namespace turradgiver_api.Dtos.Ads
{
    /// <summary>
    /// 
    /// </summary>
    public class AdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public float Price { get; set; }
    }
}