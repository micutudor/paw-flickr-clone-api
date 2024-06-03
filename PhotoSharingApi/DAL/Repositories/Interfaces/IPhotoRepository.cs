using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        List<Photo> GetAll();
        Photo GetById(int photoId);
        List<Photo> GetByTitle(string title);
    }
}
