using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<Photo> _dbSet;

        public PhotoRepository(DataContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Photo>();
        }

        public override List<Photo> GetAll()
        {
            return _dbSet.Include(photo => photo.PhotoCategories)
                .ThenInclude(photoCategory => photoCategory.Category)
                .Include(photo => photo.User)
                .ToList();
        }
    }
}
