﻿#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Auth;
using turradgiver_dal.Models;
using turradgiver_dal.Repositories;
#endregion

namespace turradgiver_bal.Services
{
    /// <summary>
    /// Provide authentification service for the API
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration, IRepository<RefreshToken> refreshTokenRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Generate the RefreshToken
        /// </summary>
        /// <returns>Return the RefreshToken as string</returns>
        private TokenDto GenerateRefreshToken()
        {
            return _jwtService.GenerateToken(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value), 1440);
        }

        /// <summary>
        /// Generate the JWToken from the user credentials
        /// </summary>
        /// <param name="user">User credentials use for JWT generation</param>
        /// <returns>Return the JWT</returns>
        private TokenDto GenerateJWToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            return _jwtService.GenerateToken(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value), 30, claims);
        }

        /// <summary>
        /// Generate the Json Web token and Refresh token 
        /// </summary>
        /// <param name="user">The user credentials use for the JWT generation</param>
        /// <returns>The AuthCredentialDtos</returns>
        private async Task<AuthCredentialDto> Authenticate(User user)
        {
            string token = GenerateJWToken(user).Token;
            string refreshToken = GenerateRefreshToken().Token;

            // Create the RefreshToken 
            RefreshToken rftoken = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id
            };

            await _refreshTokenRepository.CreateAsync(rftoken);

            return new AuthCredentialDto
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// Hash a specific password received in parameter
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hash of the password in bytes</returns>
        private static byte[] HashPassword(string password)
        {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Check if the raw password provided is valid with the user hash password provided too
        /// </summary>
        /// <param name="password">The raw password to test</param>
        /// <param name="userHashPassword">The user hash password use in order to test the password</param>
        /// <returns>return a boolean, true if the password is valid, false if not.</returns>
        private static Boolean CheckPassword(string password, byte[] userHashPassword)
        {
            string hashPassword = Encoding.UTF8.GetString(HashPassword(password));
            string userPassword = Encoding.UTF8.GetString(userHashPassword);

            return hashPassword.CompareTo(userPassword) == 0;
        }

        /// <summary>
        /// Register a new user if he doesn't exist yet.
        /// If it's not the case it will return the AuthCredentialDto i.e jwtToken 
        /// </summary>
        /// <param name="user">The user data to register</param>
        /// <param name="password">The password which will be added to the user data before being hashed</param>
        /// <returns>
        /// Return the AuthCredentialDtos i.e the Jwtoken once the user will be register
        /// Return a failed Response if the user already exists in the database
        /// </returns>
        public async Task<Response<AuthCredentialDto>> RegisterAsync(UserSignUpDto userSignUpDto)
        {
            Response<AuthCredentialDto> res = new Response<AuthCredentialDto>();
            User user = new User() { Username = userSignUpDto.Username, Email = userSignUpDto.Email };
            User checkUser = (await _userRepository.GetByConditionAsync((u => u.Email.CompareTo(user.Email) == 0))).FirstOrDefault();
            if (checkUser != null)
            {
                res.Success = false;
                res.Message = "User already exists";
                return res;
            }
            user.Password = HashPassword(userSignUpDto.Password);

            await _userRepository.CreateAsync(user);
            res.Data = await Authenticate(user);
            return res;
        }

        /// <summary>
        /// Check if the user exist in the database, with valid email and valid password
        /// </summary>
        /// <param name="email">The email of the user to login</param>
        /// <param name="password">The password of the user to validate</param>
        /// <returns>
        /// Return AuthCredentialDtos i.e JWToken
        /// </returns>
        public async Task<Response<AuthCredentialDto>> LoginAsync(string email, string password)
        {
            Response<AuthCredentialDto> res = new Response<AuthCredentialDto>();
            User user = (await _userRepository.GetByConditionAsync((u => u.Email.CompareTo(email) == 0))).FirstOrDefault();

            if (user == null)
            {
                res.Success = false;
                res.Message = "Invalid Email";
            }
            else if (!CheckPassword(password, user.Password))
            {
                res.Success = false;
                res.Message = "Invalid Password";
            }
            else
            {
                res.Data = await Authenticate(user);
            }
            return res;
        }

        /// <summary>
        /// Check if the RefreshToken is valid, and if it exists in the database.
        /// If the RefreshToken is valid then it will delete it and generate a new pair Token and RefreshToken
        /// </summary>
        /// <param name="rToken">The refreshToken use for exchange</param>
        /// <returns>Return AuthCredentialDtos with JWT and RefreshToken</returns>
        public async Task<Response<AuthCredentialDto>> RefreshToken(ExchangeRefreshTokenDto refreshDto)
        {
            Response<AuthCredentialDto> res = new Response<AuthCredentialDto>();
            if (!_jwtService.ValidateToken(refreshDto.RefreshToken, Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value)))
            {
                res.Success = false;
                res.Message = "Invalid RefreshToken validate";
                return res;
            }
            RefreshToken refreshToken = (await _refreshTokenRepository.IncludeAsync((r) => r.User)).Where((r) => r.Token.CompareTo(refreshDto.RefreshToken) == 0).FirstOrDefault();
            if (refreshToken == null)
            {
                res.Success = false;
                res.Message = "Invalid RefreshToken null";
                return res;
            }

            await _refreshTokenRepository.DeleteByIdAsync(refreshToken.Id);

            res.Data = await Authenticate(refreshToken.User);
            return res;
        }
    }

}
