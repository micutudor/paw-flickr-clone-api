using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(DataContext dbContext) : base(dbContext) { }

        public List<Album> GetAllUserAlbums(int userId)
        {
            return _dbSet.Where(item => item.user_id == userId)
                .Include(album => album.PhotoAlbums)
                .ThenInclude(photoAlbum => photoAlbum.Photo)
                .ToList();
        }
    }
}
