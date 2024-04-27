using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class PhotoCategoryRepository : BaseRepository<PhotoCategory>, IPhotoCategoryRepository
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<PhotoCategory> _dbSet;

        public PhotoCategoryRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<PhotoCategory>();
        }

        public List<PhotoCategory> GetPhotosByCategory(int categoryId)
        {
            return _dbSet.Where(item => item.category_id == categoryId)
                .Include(photoCategory => photoCategory.Photo)
                .ThenInclude(photo => photo.User)
                .Include(photoCategory => photoCategory.Photo)
                .ThenInclude(photo => photo.PhotoCategories)
                .ThenInclude(photoCategory => photoCategory.Category)
                .ToList();
        }
    }
}
