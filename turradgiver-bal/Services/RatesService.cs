using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using turradgiver_dal.Models;
using System.Linq.Expressions;
using turradgiver_dal.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Rates;


namespace turradgiver_bal.Services
{
    /// <summary>
    /// Class <c>RatesService</c>   Provide rate to the corresponding ad.
    ///                             Create and remove rating.
    /// </summary>
    public class RatesService : IRatesService
    {
        private readonly IRepository<Rating> _rateRepository;
        private readonly IRepository<Ad> _adRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RatesService(IRepository<Rating> rateRepository, IRepository<Ad> adRepository, IMapper mapper, ILogger<RatesService> logger)
        {
            _rateRepository = rateRepository;
            _adRepository = adRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all rates to the corresponding ad.
        /// </summary>
        /// <param name="createAdDto">The data of the ad to create</param>
        /// <param name="userId">The userId who created the add</param>
        /// <returns>Return rates in success</returns>
        // public async Task<Response<Rating>> GetRatesAsync(Guid id){

        // }
        public async Task<Response<RateDto>> CreateAsync(CreateRateDto createRateDto, Guid userId)
        {
            Response<RateDto> res = new Response<RateDto>();
            bool succes = await HandleRate(createRateDto.AdId, createRateDto.Rate);
            if (succes == false){
                res.Success = false;
                res.Message = "Add doesn't exist";
                return res;
            }
            Rating newRate = new Rating();
            newRate = _mapper.Map<Rating>(createRateDto);
            newRate.UserId = userId;
            await _rateRepository.CreateAsync(newRate);
            res.Data = _mapper.Map<RateDto>(newRate);
            res.Success = succes;
            return res;
        }

        public async Task<bool> HandleRate(Guid AdId, int newRate){
            IEnumerable<RateDto> rates = (await GetRatesbyAdId(AdId)).Data;
            int rateNumber = rates.Count<RateDto>();
            Ad ad = await _adRepository.GetByIdAsync(AdId);
            if( ad == null){
                return false;
            }
            ad.Rate = (rateNumber*ad.Rate + newRate)/(rateNumber+1);
            try {
                await _adRepository.UpdateAsync(ad);
            }catch(Exception){
                return false;
            }
            return true;

        } 
        public async Task<Response<IEnumerable<RateDto>>> GetRatesbyAdId(Guid AdId){
            Response<IEnumerable<RateDto>> res = new Response<IEnumerable<RateDto>>(); 
            var rates = await _rateRepository.GetByConditionAsync(e => e.AdId == AdId );
            res.Data = _mapper.Map<List<RateDto>>(rates);
            return res;
        }
        public async Task<Response<IEnumerable<RateDto>>> GetRatesAsync(Guid AdId, GetCommentsDto page){
            Response<IEnumerable<RateDto>> res = new Response<IEnumerable<RateDto>>();
            Ad ad = await _adRepository.GetByIdAsync(AdId);
            if (ad == null){
                res.Success = false;
                res.Message = "Ad doesn't exit";
                return res;
            }
            Expression<Func<Rating, bool>> exp = AdId != null
                ? (e => e.AdId == AdId)
                : (e => e.AdId == AdId);
            var rates = (await _rateRepository.GetByRangeAsync((page.page-1)*2,2,exp));
            res.Data = _mapper.Map<List<RateDto>>(rates);
            
            return res;
        }
        
    }
}
