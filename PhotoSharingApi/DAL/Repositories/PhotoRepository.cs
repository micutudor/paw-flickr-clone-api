using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DataContext dbContext) : base(dbContext) { }

        public override List<Photo> GetAll()
        {
            return _dbSet.Include(photo => photo.PhotoCategories)
                .ThenInclude(photoCategory => photoCategory.Category)
                .Include(photo => photo.User)
                .ToList();
        }

        public Photo GetById(int photoId)
        {
            return GetAll().Find(item => item.photo_id == photoId);
        }

        public List<Photo> GetByTitle(string titlePart)
        {
            string lowerTitlePart = titlePart.ToLower();
            return GetAll()
                    .Where(photo => photo.title.ToLower().Contains(lowerTitlePart))
                    .ToList();
        }
    }
}
