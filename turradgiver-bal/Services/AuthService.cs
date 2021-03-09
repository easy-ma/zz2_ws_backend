#region usings
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

using turradgiver_dal.Models;
using turradgiver_dal.Repositories;

using turradgiver_bal.Dtos;
using turradgiver_bal.Dtos.Auth;
#endregion

namespace turradgiver_bal.Services
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
        /// Generate the token from claims provided, with a specific ammount of time for expirtation provided as parameter
        /// And with a secretKey received as parameter
        /// </summary>
        /// <param name="secretKey">The secretKey used for the creation of the singninCredentials</param>
        /// <param name="expMinutes">Number of minutes before the expiration of the token generated</param>
        /// <param name="claims">The claims to encode in the token, null if not provided</param>
        /// <returns>Return the string representation of the token</returns>
        private TokenDto GenerateToken(byte[] secretKey,double expMinutes,IEnumerable<Claim> claims=null)
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
            return new TokenDto(){
                Token= tokenHandler.WriteToken(tokenHandler.CreateToken(token)),
                Expires = (DateTime)token.Expires
            };
        }

        /// <summary>
        /// Validate the refreshToken 
        /// </summary>
        /// <param name="refreshToken">RefreshToken to validate</param>
        /// <returns>Return true if the RefreshToken is validate, false if not</returns>
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

        /// <summary>
        /// Generate the RefreshToken
        /// </summary>
        /// <returns>Return the RefreshToken as string</returns>
        private TokenDto GenerateRefreshToken()
        {
            return GenerateToken(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value), 1440);
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
            return GenerateToken(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value), 30,claims);
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
            RefreshToken rftoken= new RefreshToken(){ 
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
            User user = new User(userSignUpDto.Username, userSignUpDto.Email);
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

        /// <summary>
        /// Check if the RefreshToken is valid, and if it exists in the database.
        /// If the RefreshToken is valid then it will delete it and generate a new pair Token and RefreshToken
        /// </summary>
        /// <param name="rToken">The refreshToken use for exchange</param>
        /// <returns>Return AuthCredentialDtos with JWT and RefreshToken</returns>
        public async Task<Response<AuthCredentialDto>> RefreshToken(ExchangeRefreshTokenDto refreshDto){
            Response<AuthCredentialDto> res = new Response<AuthCredentialDto>();
            if(!ValidateToken(refreshDto.RefreshToken)){
                res.Success = false;
                res.Message = "Invalid RefreshToken";
                return res;
            }
            RefreshToken refreshToken = (await _refreshTokenRepository.IncludeAsync((r)=> r.User)).Where((r)=> r.Token.CompareTo(refreshDto.RefreshToken) == 0).FirstOrDefault();
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
