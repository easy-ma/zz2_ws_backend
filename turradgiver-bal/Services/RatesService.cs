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

        public async Task<Response<RateDto>> CreateAsync(CreateRateDto createRateDto, Guid userId)
        {
            Response<RateDto> res = new Response<RateDto>();
            bool succes = await HandleRate(createRateDto.AdId, createRateDto.Rate);
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

        public async Task<bool> HandleRate(Guid AdId, int newRate)
        {
            IEnumerable<RateDto> rates = (await GetRatesbyAdId(AdId)).Data;
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
        public async Task<Response<IEnumerable<RateDto>>> GetRatesbyAdId(Guid AdId)
        {
            Response<IEnumerable<RateDto>> res = new Response<IEnumerable<RateDto>>();
            var rates = await _rateRepository.GetByConditionAsync(e => e.AdId == AdId);
            res.Data = _mapper.Map<List<RateDto>>(rates);
            return res;
        }
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
            _logger.LogInformation(page.Page.ToString());

            Expression<Func<Rating, bool>> exp = e => e.AdId == AdId;
            var rates = await _rateRepository.GetByRangeAsync(2 * (page.Page - 1), 2, exp);
            res.Data = _mapper.Map<List<RateDto>>(rates);

            return res;
        }

    }
}
