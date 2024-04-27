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

        private readonly IBaseRepository<User> _baseRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IBaseRepository<User> baseRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
            _userRepository = userRepository;

        }

        public List<UserModel> GetAll() => _mapper.Map<List<UserModel>>(_baseRepository.GetAll());

        public string HashPassword(string password) 
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        public async Task CreateUser(UserModel user)
        {
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

            await _userRepository.Create(newUser);
        }

        public async Task<string> GetUsernameById(int userid)
        {
            return await _userRepository.GetUsernameById(userid);
        }
    }
}
