using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IAlbumRepository : IBaseRepository<Album>
    {
        List<Album> GetAllUserAlbums(int userId);
        Album GetAlbumPhotos(int albumId);
    }
}
