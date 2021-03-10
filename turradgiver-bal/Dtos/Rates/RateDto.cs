using System;


namespace turradgiver_bal.Dtos.Rates
{
    public class RateDto
    {
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public int Rate { get; set; }
    }
}
