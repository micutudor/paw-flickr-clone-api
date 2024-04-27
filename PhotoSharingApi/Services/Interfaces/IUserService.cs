using PhotoSharingApi.Models;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetAll();
        Task CreateUser (UserModel user);
        Task<string> GetUsernameById(int userid);

    }
}
