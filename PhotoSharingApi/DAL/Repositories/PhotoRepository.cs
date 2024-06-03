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
            return _dbSet.Find(photoId);
        }

        public List<Photo> GetByTitle(string titlePart)
        {
            string lowerTitlePart = titlePart.ToLower();
            return _dbSet.Where(photo => photo.title.ToLower().Contains(lowerTitlePart)).ToList();
        }
    }
}
