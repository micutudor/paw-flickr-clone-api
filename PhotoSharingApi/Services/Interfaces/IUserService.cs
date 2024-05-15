using PhotoSharingApi.Models;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(UserModel user);
        Task<string> GetUsernameById(int userid);
    }
}
