using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using turradgiver_api.Utils;
using AutoMapper;
using turradgiver_api.Dtos.Ads;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections;

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

        private readonly int itemPerPage = 4;

        public AdsService(IRepository<Ad> addsRepository, IMapper mapper, ILogger<AdsService> logger)
        {
            _adRepository = addsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create an add from the data received
        /// </summary>
        /// <param name="createAdDto">The data of the ad to create</param>
        /// <param name="userId">The userId who created the add</param>
        /// <returns>Return a success response if the ad is created</returns>
        public async Task<Response<Ad>> CreateAsync(CreateAdDto createAdDto, Guid userId)
        {
            Response<Ad> res = new Response<Ad>();
            res.Data = _mapper.Map<Ad>(createAdDto);
            res.Data.UserId = userId;
            await _adRepository.CreateAsync(res.Data);
            return res;
        }

        /// <summary>
        /// Retrieve the ad related to the id provided as parameter
        /// </summary>
        /// <param name="id">The id of the ad to return</param>
        /// <returns>Return the ad with the id provided within a success response, return a fail response if not found</returns>
        public async Task<Response<AdDto>> GetAdAsync(Guid id)
        {
            Response<AdDto> res = new Response<AdDto>();
            Ad ad = await _adRepository.GetByIdAsync(id);
            if (ad == null){
                res.Success = false;
                res.Message = "Ad not found.";
                return res;
            }
            res.Data = _mapper.Map<AdDto>(ad);
            res.Message = "Ad found.";
            return res;
        }
        
        /// <returns>Return a success response if deleted</returns>

        /// <summary>
        /// Delete the ad related to the id provided as parameter
        /// </summary>
        /// <param name="adId">The id of the ad to delete</param>
        /// <param name="userId">The userId who wanted to delete the ad</param>
        /// <returns></returns>
        public async Task<Response<Ad>> RemoveUserAdAsync(Guid adId, Guid userId)
        {
            Response<Ad> res = new Response<Ad>();
            if (await CheckIfAdExistAndBelongToUserAsync(adId,userId)) {
                await _adRepository.DeleteByIdAsync(adId);
                res.Message = "Remove succeed";
                return res;
            }
            return new Response<Ad>(){ 
                Success = false,
                Message = "Ad not found for this user."
            };
        }

        /// <summary>
        /// Check if an ad with adId exists and belong to the User with the userId.
        /// </summary>
        /// <param name="adId">The ad id</param>
        /// <param name="userId">The user id</param>
        /// <returns>Return true if the ad exists and belong to the user, false if not.</returns>
        public async Task<bool> CheckIfAdExistAndBelongToUserAsync(Guid adId, Guid userId) 
        {
            Ad ad = await _adRepository.GetByIdAsync(adId);
            if (ad != null && ad.UserId == userId ) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns all ads paginated and depending criterias
        /// </summary>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public async Task<Response<IEnumerable<AdDto>>> GetAdsAsync(SearchDto criterias)
        {
            IQueryable<Ad> ads;
            if (criterias.Search != null)
            {
                ads = await _adRepository.GetByRangeAsync(this.itemPerPage * (criterias.Page - 1), this.itemPerPage, e => e.Name.Contains(criterias.Search) || e.Description.Contains(criterias.Search));
            } 
            else
            {
                ads = await _adRepository.GetByRangeAsync(this.itemPerPage * (criterias.Page - 1), this.itemPerPage);

            }

            // Successful : no data = empty array
            Response<IEnumerable<AdDto>> res = new Response<IEnumerable<AdDto>>() { Data = _mapper.Map<List<AdDto>>(ads) };

            return res;
        }


        /// <summary>
        /// Returns all ads paginated and depending criterias for a specific User
        /// </summary>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public async Task<Response<IEnumerable<AdDto>>> GetUserAdsAsync(Guid userId, SearchDto criterias)
        {
            IQueryable<Ad> ads;
            if (criterias.Search != null)
            {
                ads = await _adRepository.GetByRangeAsync(this.itemPerPage * (criterias.Page - 1), this.itemPerPage, e => e.UserId == userId && (e.Name.Contains(criterias.Search) || e.Description.Contains(criterias.Search)));
            }
            else
            {
                ads = await _adRepository.GetByRangeAsync(this.itemPerPage * (criterias.Page - 1), this.itemPerPage, e => e.UserId == userId);

            }

            // Successful : no data = empty array
            Response<IEnumerable<AdDto>> res = new Response<IEnumerable<AdDto>>() { Data = _mapper.Map<List<AdDto>>(ads) };

            return res;
        }

        public async Task<Response<IQueryable<Ad>>> FilterAsync(string text)
        {
            Response<IQueryable<Ad>> res = new Response<IQueryable<Ad>>();

            IQueryable<Ad> data = await _adRepository.GetByConditionAsync(e => (e.Name).Contains(text) == true || (e.Description).Contains(text) == true);
            if (!data.Any())
            {
                res.Success = false;
                res.Message = "Ads not found";
            }
            res.Data = data;
            res.Message = $"hmm {text} nice choice ;)";
            return res;
        }
    }

}
