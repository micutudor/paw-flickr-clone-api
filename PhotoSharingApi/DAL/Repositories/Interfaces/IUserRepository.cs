using PhotoSharingApi.DAL.Models;
using System.Runtime.InteropServices;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetById(int userid);
        Task<string> GetUsernameById(int userid);
    }
}
