using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IPhotoCategoryRepository : IBaseRepository<PhotoCategory>
    {
        List<PhotoCategory> GetPhotosByCategory(int categoryId);
    }
}
