using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using turradgiver_api.Utils;
using AutoMapper;
using turradgiver_api.Dtos.Ads;
using System;
using Microsoft.Extensions.Logging;

namespace turradgiver_api.Services
{
    /// <summary>
    /// Class <c>HomeService</c> provide adds according to the user input
    /// </summary>
    public class AdsService : IAdsService
    {
        private readonly IRepository<Ad> _adRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AdsService(IRepository<Ad> addsRepository, IMapper mapper, ILogger<AdsService> logger)
        {
            _adRepository = addsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<Ad>> CreateAsync(CreateAdDto createAdDto, Guid userId)
        {
            Response<Ad> res = new Response<Ad>();
            res.Data = _mapper.Map<Ad>(createAdDto);
            res.Data.UserId = userId;
            await _adRepository.CreateAsync(res.Data);
            return res;
        }

        public async Task<Response<Ad>> GetAdAsync(Guid id)
        {
            Response<Ad> res = new Response<Ad>();
            Ad ad = await _adRepository.GetByIdAsync(id);
            if (ad == null){
                res.Success = false;
                res.Message = "Ad not found.";
                return res;
            }
            res.Data = ad;
            res.Message = "Ad found.";
            return res;
        }

        public async Task<Response<Ad>> RemoveAsync(Guid id)
        {
            Response<Ad> res = new Response<Ad>();
            await _adRepository.DeleteByIdAsync(id);
            res.Message = "Remove succeed";
            return res;
        }

        public async Task<bool> CheckIfAdExistAndBelongToUserAsync(Guid adId, Guid userId) 
        {
            Ad ad = await _adRepository.GetByIdAsync(adId);
            if (ad != null && ad.UserId == userId ) {
                return true;
            }
            return false;
        }

        public async Task<Response<IQueryable<Ad>>> GetUserAds(Guid userId)
        {
            Response<IQueryable<Ad>> res = new Response<IQueryable<Ad>>();

            IQueryable<Ad> data = await _adRepository.GetByConditionAsync(e => e.UserId == userId);
            if (!data.Any())
            {
                res.Success = false;
                res.Message = "No ads find";
            }
            res.Data = data;
            return res;
        }

        public async Task<Response<IQueryable<Ad>>> FilterAsync(string text)
        {
            Response<IQueryable<Ad>> res = new Response<IQueryable<Ad>>();

            IQueryable<Ad> data = await _adRepository.GetByConditionAsync(e => (e.Name).Contains(text) == true || (e.Description).Contains(text) == true);
            if (!data.Any())
            {
                res.Success = false;
                res.Message = "No ads find";
            }
            res.Data = data;
            res.Message = $"hmm {text} nice choice ;)";
            return res;
        }
    }

}
