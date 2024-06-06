using PhotoSharingApi.Models;
using PhotoSharingApi.Models.Authenticate;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(CreateAccountRequestModel user);

        Task<string> GetUsernameById(int userid);

        int GetIDByUsername(string username);
    }
}
