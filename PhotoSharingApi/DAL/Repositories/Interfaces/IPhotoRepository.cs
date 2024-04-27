using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        List<Photo> GetAll();
    }
}
