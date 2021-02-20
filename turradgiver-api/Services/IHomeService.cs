using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using turradgiver_api.Utils;
using turradgiver_api.Dtos.Home;

namespace turradgiver_api.Services
{
    public interface IHomeService
    {
        Task<Response<AddsDto>> Filter(string text);
    }

}
