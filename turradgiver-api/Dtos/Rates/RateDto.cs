using System.ComponentModel.DataAnnotations;
using System;


namespace turradgiver_api.Dtos.Advices
{
    public class RateDto{
        public Guid Id { get; set; }
        public Guid AdId { get; set; }
        public Guid UserId { get; set; }
        public string comment {get; set;}
        public string user {get;set;}
        public int rate {get;set;}


    }
}