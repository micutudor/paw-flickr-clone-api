using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services.Interfaces;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PhotoSharingApi.Models.Enums;
using PhotoSharingApi.Models.Authenticate;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Helpers;

namespace PhotoSharingApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;

        private readonly IUserRepository _userRepository;

        private readonly string _jwtSecret;

        public AuthenticationService(IConfiguration config, IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;

            _userRepository = userRepository;
            _jwtSecret = config["JWTSecret"];
        }

        public async Task<AuthenticateResponseModel> Register(CreateAccountRequestModel newAccount)
        {
            string error = null;

            if (string.IsNullOrWhiteSpace(newAccount.FirstName))
            {
                error = "The first name is empty!";
            }
            else if (string.IsNullOrWhiteSpace(newAccount.LastName))
            {
                error = "The last name is empty!";
            }
            else if (string.IsNullOrWhiteSpace(newAccount.Username))
            {
                error = "The username is empty!";
            }
            else if (_userRepository.CheckIfUsernameIsUnique(newAccount.Username))
            {
                error = "This username is already taken!";
            }
            else if (string.IsNullOrWhiteSpace(newAccount.Email))
            {
                error = "The email is empty!";
            }
            else if (_userRepository.CheckIfEmailIsUnique(newAccount.Email))
            {
                error = "This email is allready used for another account!";
            }
            else if (string.IsNullOrWhiteSpace(newAccount.Password))
            {
                error = "The password is empty!";
            }

            if (error == null)
            {
                await _userService.Create(newAccount);

                return new AuthenticateResponseModel
                {
                    Successfull = true,
                    Error = null,
                    SessionJWT = GenerateJWTToken(newAccount.Username, 0),
                };
            }

            return new AuthenticateResponseModel
            {
                Successfull = false,
                Error = error,
                SessionJWT = null
            };
        }

        public AuthenticateResponseModel Login(LogInAccountRequestModel existentAccount)
        {
            var user = _userRepository.GetByUsername(existentAccount.Username);

            if (user != null && VerifyPassword(existentAccount.Password, user.password))
            {
                return new AuthenticateResponseModel
                {
                    Successfull = true,
                    Error = null,
                    SessionJWT = GenerateJWTToken(user.username, user.is_moderator)
                };
            }

            return new AuthenticateResponseModel
            {
                Successfull = false,
                Error = "Username and password didn't match!",
                SessionJWT = null
            };
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInputPassword = HashPassword.Hash(password);
            return hashedInputPassword == hashedPassword;
        }

        public string GenerateJWTToken(string username, int? isModerator)
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

        public ClaimsPrincipal ValidateJWTToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                return claimsPrincipal;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new ApplicationException("Token has expired.");
            }
        }

    }
}
