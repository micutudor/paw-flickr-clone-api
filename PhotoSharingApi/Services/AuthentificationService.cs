using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services.Interfaces;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PhotoSharingApi.Models.Enums;

namespace PhotoSharingApi.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IUserService _userService;

        private readonly IUserRepository _userRepository;

        private readonly string _jwtSecret;

        public AuthentificationService(IConfiguration config, IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;

            _userRepository = userRepository;
            _jwtSecret = config["JWTSecret"];
        }

        public void Register(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("The first name is empty!");
            }
            else if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("The last name is empty!");
            }
            else if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new ArgumentException("The username is empty!");
            }
            else if (_userRepository.CheckIfUsernameIsUnique(user.Username))
            {
                throw new ArgumentException("This username is allready taken!");
            }
            else if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("The email is empty!");
            }
            else if (_userRepository.CheckIfEmailIsUnique(user.Email))
            {
                throw new ArgumentException("This email is allready used for another account!");
            }
            else if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("The password is empty!");
            }

            _userService.Create(user);
        }

        public string Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user != null && VerifyPassword(password, user.password))
            {
                return GenerateJwtToken(user.username, user.is_moderator);
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

        private string GenerateJwtToken(string username, int? isModerator)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRolesEnum), isModerator ?? 0))
                }),
                Expires = DateTime.UtcNow.AddDays(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
