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
        private readonly IRepository<Add> _addsRepository;
        
        public AddsService(IRepository<Add> addsRepository)
        {
            _addsRepository = addsRepository;
        }

        public async Task<Response<Add>> Create(Add add){

           Response<Add> res = new Response<Add>();
           if (add.Name.CompareTo("") == 0){
                res.Success = false;
                res.Message = "Please insert title";
                return res;
            }
            res.Data = add;
            _addsRepository.Create(add);
            return res;
        }        


        public async Task<Response<Add>> Remove(int id){

           Response<Add> res = new Response<Add>();
            _addsRepository.DeleteById(id);
            res.Message = "Remove succeed";
            return res;
        }        
        
        public async Task<Response<IQueryable<Add>>> Filter( string text){
            Response<IQueryable<Add>> res = new Response<IQueryable<Add>>();

            IQueryable<Add> data = _addsRepository.GetByCondition(e => (e.Name).Contains(text) == true || (e.Description).Contains(text) == true);
            
            if(data == null){
                res.Success = false;
                res.Message = "No adds find";
            }
            res.Data = data;
            res.Message = $"hmm {text} nice choice ;)";
            return  res;  
        } 
  }

}