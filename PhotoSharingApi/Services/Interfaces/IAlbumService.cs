using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models.Albums;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IAlbumService
    {
        Task Create(CreateAlbumRequestModel album);
        GetAlbumResponseModel Get(int albumId);
        List<UserAlbumsResponseModel> GetUserAlbums(int userId);
    }
}
