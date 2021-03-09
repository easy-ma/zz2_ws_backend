using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using turradgiver_dal.Models;
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
        public async Task<Response<Rating>> CreateAsync(CreateRateDto createRateDto, Guid userId) // Guid adId
        {
            Response<Rating> res = new Response<Rating>();
            res.Data = _mapper.Map<Rating>(createRateDto);
            // res.Data.AdId = adId;
            res.Data.UserId = userId;
            await _rateRepository.CreateAsync(res.Data);
            return res;
        }

        public async Task<Response<IEnumerable<RateDto>>> GetRatesAsync(Guid AdId, int page){
            Response<IEnumerable<RateDto>> res = new Response<IEnumerable<RateDto>>();
            Ad ad = await _adRepository.GetByIdAsync(AdId);
            if (ad == null){
                res.Success = false;
                res.Message = "Ad doesn't exit";
                return res;
            }
            IEnumerable<Rating> data = (await _rateRepository.GetByConditionAsync(e => e.AdId == AdId));
            data = data.Skip((page-1)*2).Take(2);
            res.Data = _mapper.Map<List<RateDto>>(data);
            return res;
        }
        
    }
}
