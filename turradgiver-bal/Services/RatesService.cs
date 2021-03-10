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

        private const int ITEM_PER_PAGE = 2;

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
            bool succes = await CalculateNewRateAsync(createRateDto.AdId, createRateDto.Rate);
            if (!succes)
            {
                res.Success = false;
                res.Message = "Add doesn't exist";
                return res;
            }
            int rates = (await _rateRepository.GetByConditionAsync((e) => e.UserId == userId)).Count();
            if (rates > 0)
            {
                res.Success = false;
                res.Message = "Already commented";
                return res;
            }

            Rating newRate = _mapper.Map<Rating>(createRateDto);
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
        private async Task<bool> CalculateNewRateAsync(Guid AdId, int newRate)
        {
            int rateNumber = await GetRateCountbyAdIdAsync(AdId);
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
        private async Task<int> GetRateCountbyAdIdAsync(Guid AdId)
        {
            return (await _rateRepository.GetByConditionAsync((e) => e.AdId == AdId)).Count();
        }

        /// <summary>
        /// Retrieve one or two rates/comments corresponding to an ad according to the page
        /// </summary>
        /// <param name="AdId">The id of the ad the user wants to get comments</param>
        /// <param name="page">The number of the page</param>
        /// <returns>Return a response of a list (length between 1 and 2)of comments/rate</returns>
        public async Task<Response<IEnumerable<RateDto>>> GetRatesAsync(Guid AdId, PageCommentDto page)
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
            List<Rating> rates = (await _rateRepository.IncludeAsync(r => r.User)).Where(exp).OrderByDescending(x => x.CreatedDate).Skip(ITEM_PER_PAGE * (page.Page - 1)).Take(ITEM_PER_PAGE).ToList();
            
            // Should be using automapper. But no working.
            // See: https://stackoverflow.com/questions/34271334/automapper-how-to-map-nested-object
            List <RateDto> rateDtos = new List<RateDto>();
            RateDto rateDto;
            foreach (var r in rates)
            {
                rateDto = _mapper.Map<RateDto>(r);
                rateDto.Username = r.User.Username;
                rateDtos.Add(rateDto);
            }
            res.Data = rateDtos;
            return res;
        }
    }
}
