using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Utils;
using turradgiver_api.Dtos.Home;
using System.Linq;


namespace turradgiver_api.Services
{
    public interface IAddsService
    {
        // Task<Response<AddsDto>> Filter(string text);
        Task<IQueryable<Add>> Filter(string text);
        Task<Response<Add>> Create(Add adds);
    }

}
