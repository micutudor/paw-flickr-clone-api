using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using System.ComponentModel.Design;

namespace PhotoSharingApi.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetById(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<string> GetUsernameById(int userId)
        {
            var result = await this.GetById(userId);
            return result.username;
        }

        public User GetByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.username == username);
        }
    }
}
