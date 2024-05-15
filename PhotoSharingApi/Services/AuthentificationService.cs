using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.DAL;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace PhotoSharingApi.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _dataContext;
        private readonly string _jwtSecret;
        public AuthentificationService(IUserRepository userRepository,DataContext dataContext, string jwtSecret)
        {
            _userRepository = userRepository;
            _dataContext = dataContext;
            _jwtSecret = jwtSecret;
        }

        public void Register(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.First_Name))
            {
                throw new ArgumentException("The first name is empty!");
            }
            else if (string.IsNullOrWhiteSpace(user.Last_Name))
            {
                throw new ArgumentException("The last name is empty!");
            }
            else if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new ArgumentException("The username is empty!");
            }
            else if (_dataContext.Users.Where(user => user.email!.ToLower() == user.email.ToLower()).Count() > 0)
            {
                throw new ArgumentException("This username is allready taken!");
            }
            else if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("The email is empty!");
            }
            else if (_dataContext.Users.Where(user => user.email!.ToLower() == user.email.ToLower()).Count() > 0)
            {
                throw new ArgumentException("This email is allready used for another account!");
            }
            else if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("The password is empty!");
            }

            
            var newUser = new User
            {
                user_id = user.Id,
                first_name = user.First_Name,
                last_name = user.Last_Name,
                username = user.Username,
                password = HashPassword(user.Password),
                email = user.Email,
                is_moderator = user.Is_Moderator
            };

            _userRepository.Create(newUser);
        }

        public string Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if(user == null && VerifyPassword(password, user.password))
            {
                return GenerateJwtToken(user.username);
            }
            return null;   
        }

        public string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
