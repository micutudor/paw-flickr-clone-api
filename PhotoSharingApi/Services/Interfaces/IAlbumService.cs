using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models.Albums;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IAlbumService
    {
        Task Create(int userID, string name);
        List<UserAlbumsResponseModel> GetUserAlbums(int userID);
    }
}
