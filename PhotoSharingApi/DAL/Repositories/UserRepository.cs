using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using System.ComponentModel.Design;

namespace PhotoSharingApi.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext) : base(dbContext) { }

        public async Task<User> GetById(int userId)
        {
            return await _dbSet.FindAsync(userId);
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
