using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class PhotoCategoryRepository : BaseRepository<PhotoCategory>, IPhotoCategoryRepository
    {
        public PhotoCategoryRepository(DataContext dbContext) : base(dbContext) { }

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
