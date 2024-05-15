using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using PhotoSharingApi.Models.Albums;


namespace PhotoSharingApi.Services.Interfaces
{
    public interface IAuthentificationService
    {
        string Login(string username, string password);
        void Register(UserModel user);
    }
}
