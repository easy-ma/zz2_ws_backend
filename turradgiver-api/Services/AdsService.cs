// using System.Linq;
// using System.Threading.Tasks;
// using DAL.Models;
// using DAL.Repositories;
// using AutoMapper;
// using turradgiver_api.Dtos.Ads;
// using System;
// using Microsoft.Extensions.Logging;

// using turradgiver_business.Dtos;

// namespace turradgiver_api.Services
// {
//     /// <summary>
//     /// Class <c>HomeService</c> provide adds according to the user input
//     /// </summary>
//     public class AdsService : IAdsService
//     {
//         private readonly IRepository<Ad> _adRepository;
//         private readonly IMapper _mapper;
//         private readonly ILogger _logger;

//         public AdsService(IRepository<Ad> addsRepository, IMapper mapper, ILogger<AdsService> logger)
//         {
//             _adRepository = addsRepository;
//             _mapper = mapper;
//             _logger = logger;
//         }

//         /// <summary>
//         /// Create an add from the data received
//         /// </summary>
//         /// <param name="createAdDto">The data of the ad to create</param>
//         /// <param name="userId">The userId who created the add</param>
//         /// <returns>Return a success response if the ad is created</returns>
//         public async Task<Response<Ad>> CreateAsync(CreateAdDto createAdDto, Guid userId)
//         {
//             Response<Ad> res = new Response<Ad>();
//             res.Data = _mapper.Map<Ad>(createAdDto);
//             res.Data.UserId = userId;
//             await _adRepository.CreateAsync(res.Data);
//             return res;
//         }

//         /// <summary>
//         /// Retrieve the ad related to the id provided as parameter
//         /// </summary>
//         /// <param name="id">The id of the ad to return</param>
//         /// <returns>Return the ad with the id provided within a success response, return a fail response if not found</returns>
//         public async Task<Response<Ad>> GetAdAsync(Guid id)
//         {
//             Response<Ad> res = new Response<Ad>();
//             Ad ad = await _adRepository.GetByIdAsync(id);
//             if (ad == null){
//                 res.Success = false;
//                 res.Message = "Ad not found.";
//                 return res;
//             }
//             res.Data = ad;
//             res.Message = "Ad found.";
//             return res;
//         }
        
//         /// <returns>Return a success response if deleted</returns>

//         /// <summary>
//         /// Delete the ad related to the id provided as parameter
//         /// </summary>
//         /// <param name="adId">The id of the ad to delete</param>
//         /// <param name="userId">The userId who wanted to delete the ad</param>
//         /// <returns></returns>
//         public async Task<Response<Ad>> RemoveUserAdAsync(Guid adId, Guid userId)
//         {
//             Response<Ad> res = new Response<Ad>();
//             if (await CheckIfAdExistAndBelongToUserAsync(adId,userId)) {
//                 await _adRepository.DeleteByIdAsync(adId);
//                 res.Message = "Remove succeed";
//                 return res;
//             }
//             return new Response<Ad>(){ 
//                 Success = false,
//                 Message = "Ad not found for this user."
//             };
//         }

//         /// <summary>
//         /// Check if an ad with adId exists and belong to the User with the userId.
//         /// </summary>
//         /// <param name="adId">The ad id</param>
//         /// <param name="userId">The user id</param>
//         /// <returns>Return true if the ad exists and belong to the user, false if not.</returns>
//         public async Task<bool> CheckIfAdExistAndBelongToUserAsync(Guid adId, Guid userId) 
//         {
//             Ad ad = await _adRepository.GetByIdAsync(adId);
//             if (ad != null && ad.UserId == userId ) {
//                 return true;
//             }
//             return false;
//         }

//         public async Task<Response<IQueryable<Ad>>> GetUserAds(Guid userId)
//         {
//             Response<IQueryable<Ad>> res = new Response<IQueryable<Ad>>();

//             IQueryable<Ad> data = await _adRepository.GetByConditionAsync(e => e.UserId == userId);
//             if (!data.Any())
//             {
//                 res.Success = false;
//                 res.Message = "Ads not found";
//             }
//             res.Data = data;
//             return res;
//         }

//         public async Task<Response<IQueryable<Ad>>> FilterAsync(string text)
//         {
//             Response<IQueryable<Ad>> res = new Response<IQueryable<Ad>>();

//             IQueryable<Ad> data = await _adRepository.GetByConditionAsync(e => (e.Name).Contains(text) == true || (e.Description).Contains(text) == true);
//             if (!data.Any())
//             {
//                 res.Success = false;
//                 res.Message = "Ads not found";
//             }
//             res.Data = data;
//             res.Message = $"hmm {text} nice choice ;)";
//             return res;
//         }
//     }

// }
