using System;


namespace turradgiver_bal.Dtos.Rates
{
    public class RateDto
    {
        public DateTime CreatedDate { get; set; }
        public Guid AdId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
    }
}