using AutoMapper;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Helpers;
using PhotoSharingApi.Models.Authenticate;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(CreateAccountRequestModel newAccount)
        {
            var newUser = new User
            {
                first_name = newAccount.FirstName,
                last_name = newAccount.LastName,
                username = newAccount.Username,
                password = HashPassword.Hash(newAccount.Password),
                email = newAccount.Email,
                is_moderator = 0
            };

            await _userRepository.Add(newUser);
        }

        public async Task<string> GetUsernameById(int userID)
        {
            return await _userRepository.GetUsernameById(userID);
        }

        public int GetIDByUsername(string username)
        {
            return _userRepository.GetByUsername(username).user_id;
        }
    }
}
