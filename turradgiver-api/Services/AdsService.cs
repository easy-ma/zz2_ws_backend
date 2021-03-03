using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using turradgiver_api.Utils;

namespace turradgiver_api.Services
{
    /// <summary>
    /// Class <c>HomeService</c> provide adds according to the user input
    /// </summary>
    public class AdsService : IAdsService
    {
        private readonly IRepository<Ads> _addsRepository;

        public AdsService(IRepository<Ads> addsRepository)
        {
            _addsRepository = addsRepository;
        }

        public async Task<Response<Ads>> Create(Ads add)
        {

            Response<Ads> res = new Response<Ads>();
            if (add.Name.CompareTo("") == 0)
            {
                res.Success = false;
                res.Message = "Please insert title";
                return res;
            }
            res.Data = add;
            await _addsRepository.Create(add);
            return res;
        }

        public async Task<Response<Ads>> Remove(int id)
        {

            Response<Ads> res = new Response<Ads>();
            await _addsRepository.DeleteById(id);
            res.Message = "Remove succeed";
            return res;
        }

        public async Task<Response<IQueryable<Ads>>> Filter(string text)
        {
            Response<IQueryable<Ads>> res = new Response<IQueryable<Ads>>();

            IQueryable<Ads> data = await _addsRepository.GetByCondition(e => (e.Name).Contains(text) == true || (e.Description).Contains(text) == true);

            if (data == null)
            {
                res.Success = false;
                res.Message = "No adds find";
            }
            res.Data = data;
            res.Message = $"hmm {text} nice choice ;)";
            return res;
        }
    }

}
