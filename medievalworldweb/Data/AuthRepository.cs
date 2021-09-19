using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using medievalworldweb.Models;
using medievalworldweb.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace medievalworldweb.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthRepository(IUserRepository userRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            var user = await _userRepository.GetUser(username);
            if (user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
                return serviceResponse;
            }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wrong password";
                return serviceResponse;
            }
            //get jwt token and send it to client on successful login
            serviceResponse.Data = CreateToken(user);
            serviceResponse.Success = true;
            serviceResponse.Message = "Logged in successfully!";
            return serviceResponse;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashToCompareAgainst = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != hashToCompareAgainst[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Username)
           };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResponse<int>> Register(string username, string password)
        {
            ServiceResponse<int> serviceResponse = new Models.ServiceResponse<int>();
            var userExists = await UserAlreadyExists(username);
            if (userExists)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User already exists.";
                serviceResponse.Data = 0;
                return serviceResponse;
            }
            //create passwordhash and password salt and store in db
            var newUser = new User();
            GetPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.Username = username;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            await _userRepository.Adduser( newUser);
            serviceResponse.Success = true;
            serviceResponse.Message = "New user created successfully";
            serviceResponse.Data = newUser.Id;
            return serviceResponse;
        }

        private void GetPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
            }
        }

        public async Task<bool> UserAlreadyExists(string username)
        {
            var user = await _userRepository.GetUser(username);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}