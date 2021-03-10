using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Rates;
using turradgiver_dal.Models;
using turradgiver_dal.Repositories;


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
        /// Create a rate/comment from the data receive
        /// </summary>
        /// <param name="userId">The id of the user who made the request</param>
        /// <param name="createRateDto">Data for create a rate/comment</param>
        /// <returns>Return a success response if the rate/comment is created and the corresponding rate/comment</returns>
        public async Task<Response<RateDto>> CreateAsync(CreateRateDto createRateDto, Guid userId)
        {
            Response<RateDto> res = new Response<RateDto>();
            bool succes = await HandleRateAsync(createRateDto.AdId, createRateDto.Rate);
            if (succes == false)
            {
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

        /// <summary>
        /// Update the rate of an ad
        /// </summary>
        /// <param name="AdId">The id of the ad the user wants to comment</param>
        /// <param name="newRate">rate choose by the user</param>
        /// <returns>Return true if the ad has been update and false if not </returns>
        public async Task<bool> CalculateNewRateAsync(Guid AdId, int newRate)
        {
            IEnumerable<RateDto> rates = (await GetRatesbyAdIdAsync(AdId)).Data;
            int rateNumber = rates.Count<RateDto>();
            Ad ad = await _adRepository.GetByIdAsync(AdId);
            if (ad == null)
            {
                return false;
            }
            ad.Rate = (rateNumber * ad.Rate + newRate) / (rateNumber + 1);
            try
            {
                await _adRepository.UpdateAsync(ad);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// Provide the comments correponding to an ad
        /// </summary>
        /// <param name="AdId">The id of the ad the user wants to get comments</param>
        /// <returns>Return a response of a list of comments/rate</returns>
        public async Task<Response<IEnumerable<RateDto>>> GetRatesbyAdIdAsync(Guid AdId)
        {
            Response<IEnumerable<RateDto>> res = new Response<IEnumerable<RateDto>>();
            var rates = await _rateRepository.GetByConditionAsync(e => e.AdId == AdId);
            res.Data = _mapper.Map<List<RateDto>>(rates);
            return res;
        }

        /// <summary>
        /// Retrieve one or two rates/comments corresponding to an ad according to the page
        /// </summary>
        /// <param name="AdId">The id of the ad the user wants to get comments</param>
        /// <param name="page">The number of the page</param>
        /// <returns>Return a response of a list (length between 1 and 2)of comments/rate</returns>
        public async Task<Response<IEnumerable<RateDto>>> GetRatesAsync(Guid AdId, GetCommentsDto page)
        {
            Response<IEnumerable<RateDto>> res = new Response<IEnumerable<RateDto>>();
            Ad ad = await _adRepository.GetByIdAsync(AdId);
            if (ad == null)
            {
                res.Success = false;
                res.Message = "Ad doesn't exit";
                return res;
            }

            Expression<Func<Rating, bool>> exp = e => e.AdId == AdId;
            var rates = await _rateRepository.GetByRangeAsync(2 * (page.Page - 1), 2, exp);
            res.Data = _mapper.Map<List<RateDto>>(rates);

            return res;
        }

    }
}
