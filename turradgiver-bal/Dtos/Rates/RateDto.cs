using System;


namespace turradgiver_bal.Dtos.Rates
{
    /// <summary>
    /// Dto for rate sent back to frontend
    /// </summary>
    public class RateDto
    {
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public int Rate { get; set; }
    }
}
