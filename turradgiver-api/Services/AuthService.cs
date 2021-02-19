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
using turradgiver_api.Utils;

namespace turradgiver_api.Services
{
    public class AuthService : IAuthService
    {

        
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        private string CreateJsonWebToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTKey").Value));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(token));
            
        }
       

        private byte[] HashPassword (string password) {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            
        }

          private Boolean CheckPassword (string password, byte[] userHashPassword) {
            string hashPassword = Encoding.UTF8.GetString(HashPassword(password));
            string userPassword = Encoding.UTF8.GetString(userHashPassword);

            return hashPassword.CompareTo(userPassword)==0;
        }

        public async Task<Response<string>> Register(User user, string password){
           Response<string> res = new Response<string>();

            User checkUser =_userRepository.GetByCondition((u=> u.Email.CompareTo(user.Email)== 0)).FirstOrDefault();
            if (checkUser != null){
                res.Success=false;
                res.Message="User already exists";
                return res;
            }
            user.Password = HashPassword(password);
            
            _userRepository.Create(user);
            res.Data= CreateJsonWebToken(user);
            return res;
        }


         public async Task<Response<string>>  Login(string email, string password){
            Response<string> res = new Response<string>();
            User user =_userRepository.GetByCondition((u=> u.Email.CompareTo(email)== 0)).FirstOrDefault();
            
            if(user == null) {
                res.Success=false;
                res.Message= "User not found";
            } else if (!CheckPassword(password,user.Password)) {
                res.Success=false;
                res.Message= "Invalid Password";
            } else {
                res.Data = CreateJsonWebToken(user);
            }
            return res;
        }
  }

}
