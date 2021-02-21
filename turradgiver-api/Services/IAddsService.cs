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
        Task<IQueryable<Adds>> Filter(string text);
        Task<Response<Adds>> Create(Adds adds);
    }

}
