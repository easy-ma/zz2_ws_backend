﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

using turradgiver_dal.Models;

using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Ads;

namespace turradgiver_bal.Services
{
    public interface IAdsService
    {
        Task<Response<IQueryable<Ad>>> FilterAsync(string text);
        Task<Response<Ad>> CreateAsync(CreateAdDto createAdDto, Guid userId);
        Task<Response<Ad>> RemoveUserAdAsync(Guid adId, Guid userId);
        Task<Response<AdDto>> GetAdAsync(Guid id);
        Task<Response<IEnumerable<AdDto>>> GetAdsAsync(SearchDto criterias);
        Task<Response<IEnumerable<AdDto>>> GetUserAdsAsync(Guid userId, SearchDto criterias);
    }
}