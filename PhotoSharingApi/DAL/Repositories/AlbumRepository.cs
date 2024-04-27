using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<Album> _dbSet;

        public AlbumRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Album>();
        }

        public List<Album> GetAllUserAlbums(int userId)
        {
            return _dbSet.Where(item => item.user_id == userId)
                .Include(album => album.PhotoAlbums)
                .ToList();
        }

        public Album GetAlbumPhotos(int albumId)
        {
            return _dbSet.Where(item => item.album_id == albumId)
                .Include(album => album.PhotoAlbums)
                .ThenInclude(photoAlbum => photoAlbum.Photo)
                .Include(album => album.User)
                .FirstOrDefault();
        }
    }
}
