﻿using System;
using System.ComponentModel.DataAnnotations;

namespace turradgiver_bal.Dtos.Auth
{
    /// <summary>
    /// DTO for the JWT refresh token
    /// </summary>
    public class ExchangeRefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }


    /// <summary>
    /// DTO for the JWT token
    /// </summary>
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
