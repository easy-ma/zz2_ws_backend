using System;
using turradgiver_api.Dtos.Home;
using DAL.Repositories;
using System.Threading.Tasks;
using turradgiver_api.Utils;


namespace turradgiver_api.Services
{
    /// <summary>
    /// Class <c>HomeService</c> provide adds according to the user input
    /// </summary>
    public class HomeService : IHomeService
    {
        // private readonly IRepository<Adds> _addsRepository;

        public async Task<Response<AddsDto>> Filter( string text){
            Response<AddsDto> res = new Response<AddsDto>();
            return res;
        }


  }

}