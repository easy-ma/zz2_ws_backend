using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using turradgiver_api.Responses.Auth;
using turradgiver_api.Utils;
using Microsoft.Extensions.Logging;

namespace turradgiver_api.Services
{
    /// <summary>
    /// Class <c>AuthService</c> provide authentification service for the API
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration,IRepository<RefreshToken> refreshTokenRepository, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _refreshTokenRepository=refreshTokenRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="expMinutes"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        private string GenerateToken(byte[] secretKey,double expMinutes,IEnumerable<Claim> claims=null)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(secretKey);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(expMinutes),
                SigningCredentials = credentials
            };
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(token));
        }

        public bool ValidateToken(string refreshToken){
            TokenValidationParameters validationParameters= new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew= TimeSpan.Zero,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try{
                tokenHandler.ValidateToken(refreshToken,validationParameters, out validatedToken);
                return true;
            }catch(Exception){
                return false;
            }
        }

        private string GenerateRefreshToken()
        {
            return GenerateToken(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value), 1440);
        }

        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            return GenerateToken(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value), 30,claims);
        }

        private async Task<AuthCredential> Authenticate(User user)
        {
            string token = GenerateToken(user);
            string refreshToken = GenerateRefreshToken();
           
            // Create the RefreshToken 
            RefreshToken rftoken= new RefreshToken(){ 
                Token = refreshToken,
                UserId = user.Id
            };

            await _refreshTokenRepository.CreateAsync(rftoken);
            
            return new AuthCredential
            {
                Token=token,
                RefreshToken=refreshToken
            };
        }

        /// <summary>
        /// Hash a specific password received in parameter
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hash of the password in bytes</returns>
        private byte[] HashPassword(string password)
        {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Check if the raw password provided is valid with the user hash password provided too
        /// </summary>
        /// <param name="password">The raw password to test</param>
        /// <param name="userHashPassword">The user hash password use in order to test the password</param>
        /// <returns>return a boolean, true if the password is valid, false if not.</returns>
        private Boolean CheckPassword(string password, byte[] userHashPassword)
        {
            string hashPassword = Encoding.UTF8.GetString(HashPassword(password));
            string userPassword = Encoding.UTF8.GetString(userHashPassword);

            return hashPassword.CompareTo(userPassword) == 0;
        }

        /// <summary>
        /// Register a new user if he doesn't exist yet.
        /// If it's not the case it will return the AuthCredential i.e jwtToken 
        /// </summary>
        /// <param name="user">The user data to register</param>
        /// <param name="password">The password which will be added to the user data before being hashed</param>
        /// <returns>
        /// Return the AuthCredentials i.e the Jwtoken once the user will be register
        /// Return a failed Response if the user already exists in the database
        /// </returns>
        public async Task<Response<AuthCredential>> RegisterAsync(User user, string password)
        {
            Response<AuthCredential> res = new Response<AuthCredential>();

            User checkUser = (await _userRepository.GetByConditionAsync((u => u.Email.CompareTo(user.Email) == 0))).FirstOrDefault();
            if (checkUser != null)
            {
                res.Success = false;
                res.Message = "User already exists";
                return res;
            }
            user.Password = HashPassword(password);

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
        /// Return AuthCredentials i.e JWToken
        /// </returns>
        public async Task<Response<AuthCredential>> LoginAsync(string email, string password)
        {
            Response<AuthCredential> res = new Response<AuthCredential>();
            User user = (await _userRepository.GetByConditionAsync((u => u.Email.CompareTo(email) == 0))).FirstOrDefault();

            if (user == null)
            {
                res.Success = false;
                res.Message = "User not found";
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


        public async Task<Response<AuthCredential>> RefreshToken(string rToken){
            Response<AuthCredential> res = new Response<AuthCredential>();
            if(!ValidateToken(rToken)){
                res.Success = false;
                res.Message = "Invalid RefreshToken";
                return res;
            }
            RefreshToken refreshToken = (await _refreshTokenRepository.IncludeAsync((r)=> r.User)).Where((r)=> r.Token.CompareTo(rToken) == 0).FirstOrDefault();
            if (refreshToken == null)
            {
                res.Success = false;
                res.Message = "Invalid RefreshToken";
                return res;
            }

            await _refreshTokenRepository.DeleteByIdAsync(refreshToken.Id);
            
            res.Data = await Authenticate(refreshToken.User);
            return res;
        }
    }

}
