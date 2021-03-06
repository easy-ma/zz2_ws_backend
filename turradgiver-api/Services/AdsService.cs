using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using turradgiver_api.Utils;
using AutoMapper;
using turradgiver_api.Dtos.Ads;

namespace turradgiver_api.Services
{
    /// <summary>
    /// Class <c>HomeService</c> provide adds according to the user input
    /// </summary>
    public class AdsService : IAdsService
    {
        private readonly IRepository<Ads> _adRepository;
        private readonly IMapper _mapper;

        public AdsService(IRepository<Ads> addsRepository, IMapper mapper)
        {
            _adRepository = addsRepository;
            _mapper = mapper;
        }

        public async Task<Response<Ads>> CreateAsync(CreateAdDto createAdDto, int userId)
        {
            Response<Ads> res = new Response<Ads>();
            res.Data = _mapper.Map<Ads>(createAdDto);
            res.Data.UserId = userId;
            await _adRepository.CreateAsync(res.Data);
            return res;
        }

        public async Task<Response<Ads>> GetAdAsync(int id)
        {
            Response<Ads> res = new Response<Ads>();
            res.Data = await _adRepository.GetByIdAsync(id);
            res.Message = "Ad found.";
            return res;
        }

        public async Task<Response<Ads>> RemoveAsync(int id)
        {
            Response<Ads> res = new Response<Ads>();
            await _adRepository.DeleteByIdAsync(id);
            res.Message = "Remove succeed";
            return res;
        }

        public async Task<bool> CheckIfAdBelongToUserAsync(int adId, int userId) 
        {
            Ads ad = await _adRepository.GetByIdAsync(adId);
            if(ad.UserId == userId){
                return true;
            }
            return false;
        }

        public async Task<Response<IQueryable<Ads>>> GetUserAds(int userId)
        {
            Response<IQueryable<Ads>> res = new Response<IQueryable<Ads>>();

            IQueryable<Ads> data = await _adRepository.GetByConditionAsync(e => e.UserId == userId);
            if (!data.Any())
            {
                res.Success = false;
                res.Message = "No ads find";
            }
            res.Data = data;
            return res;
        }

        public async Task<Response<IQueryable<Ads>>> FilterAsync(string text)
        {
            Response<IQueryable<Ads>> res = new Response<IQueryable<Ads>>();

            IQueryable<Ads> data = await _adRepository.GetByConditionAsync(e => (e.Name).Contains(text) == true || (e.Description).Contains(text) == true);
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
