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
            return _dbSet.FirstOrDefault(u => u.username == username);
        }

        public bool CheckIfEmailIsUnique(string email)
        {
            return _dbSet.Where(user => user.email.ToLower() == email!.ToLower()).Count() > 0;
        }

        public bool CheckIfUsernameIsUnique(string username) 
        {
            return _dbSet.Where(user => user.username.ToLower() == username!.ToLower()).Count() > 0;
        }
    }
}
