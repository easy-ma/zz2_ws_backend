#region usings
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using System;

using AutoMapper;

using turradgiver_dal.Models;
using turradgiver_dal.Repositories;

using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Ads;

#endregion

namespace turradgiver_bal.Services
{
    /// <summary>
    /// Provide adds according to the user input
    /// </summary>
    public class AdsService : IAdsService
    {
        private readonly IRepository<Ad> _adRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private const int ITEM_PER_PAGE = 4;

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
            if (ad == null)
            {
                res.Success = false;
                res.Message = "Ad not found.";
                return res;
            }
            res.Data = _mapper.Map<AdDto>(ad);
            res.Message = "Ad found.";
            return res;
        }

        /// <summary>
        /// Delete the ad related to the id provided as parameter
        /// </summary>
        /// <param name="adId">The id of the ad to delete</param>
        /// <param name="userId">The userId who wanted to delete the ad</param>
        /// <returns>Return a success response if deleted</returns>
        public async Task<Response<Ad>> RemoveUserAdAsync(Guid adId, Guid userId)
        {
            Response<Ad> res = new Response<Ad>();
            if (await CheckIfAdExistAndBelongToUserAsync(adId, userId))
            {
                await _adRepository.DeleteByIdAsync(adId);
                res.Message = "Remove succeed";
                return res;
            }
            return new Response<Ad>()
            {
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
            if (ad != null && ad.UserId == userId)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns all ads paginated and depending on the expression received as parameter
        /// </summary>
        /// <param name="expression">The expression user for filtering the request</param>
        /// <param name="page">The page requested by the user</param>
        /// <param name="nb">The number of element to print in one page, <c>ITEM_PER_PAGE</<c> by default</param>
        /// <returns>Return the list of Ad that match the request</returns>
        private async Task<Response<IEnumerable<AdDto>>> Search(Expression<Func<Ad, bool>> expression, int page, int nb = ITEM_PER_PAGE)
        {
            var ads = await _adRepository.GetByRangeAsync(nb * (page - 1), nb, expression);
            Response<IEnumerable<AdDto>> res = new Response<IEnumerable<AdDto>>() { Data = _mapper.Map<List<AdDto>>(ads) };
            return res;
        }

        /// <summary>
        /// Returns all ads paginated and depending criterias
        /// </summary>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public async Task<Response<IEnumerable<AdDto>>> GetAdsAsync(SearchDto criterias)
        {
            Expression<Func<Ad, bool>> exp = criterias.Search != null
                ? (e => e.Name.Contains(criterias.Search) || e.Description.Contains(criterias.Search))
                : (e => true);

            return await Search(exp, criterias.Page);
        }

        /// <summary>
        /// Returns all ads paginated and depending criterias for a specific User
        /// </summary>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public async Task<Response<IEnumerable<AdDto>>> GetUserAdsAsync(Guid userId, SearchDto criterias)
        {
            Expression<Func<Ad, bool>> exp = criterias.Search != null
                ? (e => e.UserId == userId && e.Name.Contains(criterias.Search) || e.Description.Contains(criterias.Search))
                : (e => e.UserId == userId);

            return await Search(exp, criterias.Page);
        }

        /// <summary>
        /// Get adds filtered depending on a text input
        /// </summary>
        /// <param name="text">The text to use to filter the ads</param>
        /// <returns>All ads that match the string condition</returns>
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
