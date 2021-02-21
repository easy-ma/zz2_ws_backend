using System;
using turradgiver_api.Dtos.Home;
using DAL.Repositories;
using System.Threading.Tasks;
using turradgiver_api.Utils;
using DAL.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;




namespace turradgiver_api.Services
{
    /// <summary>
    /// Class <c>HomeService</c> provide adds according to the user input
    /// </summary>
    public class AddsService : IAddsService
    {
        private readonly IRepository<Adds> _addsRepository;
        
        public AddsService(IRepository<Adds> addsRepository)
        {
            _addsRepository = addsRepository;
        }

        public async Task<Response<Adds>> Create(Adds add){

           Response<Adds> res = new Response<Adds>();
           
            res.Data = add;
            _addsRepository.Create(add);
            return res;
        }        
        
        public async Task<IQueryable<Adds>> Filter( string text){
             // if (add.Name.CompareTo("") == 0){
            //     res.Success = false;
            //     return res;
            // }
            return _addsRepository.GetByCondition(e => e.Description.Contains(text) || e.Name.Contains(text));
        } 


  }

}