using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services.Interfaces;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace PhotoSharingApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task Create(UserModel user)
        {
            var newUser = new User
            {
                first_name = user.FirstName,
                last_name = user.LastName,
                username = user.Username,
                password = HashPassword(user.Password),
                email = user.Email,
                is_moderator = 0
            };

            await _userRepository.Add(newUser);
        }

        public async Task<string> GetUsernameById(int userid)
        {
            return await _userRepository.GetUsernameById(userid);
        }

        #region UTILS
        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
        #endregion
    }
}
