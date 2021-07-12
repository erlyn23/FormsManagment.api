using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Morocoto.Infraestructure.Tools;
using SimpleJwt.DbContexts;
using SimpleJwt.Models.Entities;
using SimpleJwt.Models.Requests;
using SimpleJwt.Models.Responses;
using SimpleJwt.Repositories.Contracts;
using SimpleJwt.Services.Contracts;
using SimpleJwt.Tools;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJwt.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly FileManagment _fileManagment;

        public AccountService(IUserRepository userRepository, IConfiguration configuration, FileManagment fileManagment)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _fileManagment = fileManagment;
        }

        public async Task<AccountRequest> SaveUser(AccountRequest accountRequest)
        {
            try
            {
                User userEntity = new User()
                {
                    Name = accountRequest.Name,
                    LastName = accountRequest.LastName,
                    Email = accountRequest.Email,
                    Password = Encryption.Encrypt(accountRequest.Password)
                };
                if (!string.IsNullOrEmpty(accountRequest.ProfilePhoto))
                {
                    var profilePhoto = await _fileManagment.UploadImageAsync($"{userEntity.Name}_{userEntity.LastName}", accountRequest.ProfilePhoto);
                    userEntity.ProfilePhoto = profilePhoto;
                }
                else
                    userEntity.ProfilePhoto = "notdefined";
                
                await _userRepository.AddAsync(userEntity);
                await _userRepository.SaveChangesAsync();

                accountRequest.UserId = userEntity.UserId;
                return accountRequest;
            }
            catch
            {
                throw;
            }
        }

        public async Task<AccountResponse> Login(AuthRequest authRequest)
        {
            try
            {
                var user = await _userRepository
                    .GetOneAsync(u => u.Email.Equals(authRequest.Email) && 
                    u.Password.Equals(Encryption.Encrypt(authRequest.Password)));

                if (user != null)
                    return new AccountResponse() { Email = user.Email, Token = BuildToken(user) };
                
                throw new Exception("Usuario o contraseña incorrecta");
            }
            catch
            {
                throw;
            }
        }

        private string BuildToken(User user)
        {
            var jwtSecurtyTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["MySecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("id", user.UserId.ToString())
            };

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMonths(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtSecurtyTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurtyTokenHandler.WriteToken(token);
        }
    }
}
