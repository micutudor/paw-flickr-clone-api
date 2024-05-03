using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class PhotoAlbumRepository : BaseRepository<PhotoAlbum>, IPhotoAlbumRepository
    {
        public PhotoAlbumRepository(DataContext dbContext) : base(dbContext) { }
    }
}
