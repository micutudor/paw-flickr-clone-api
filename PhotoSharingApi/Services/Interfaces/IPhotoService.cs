using PhotoSharingApi.Models.Photos;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IPhotoService
    {
        Task Add(AddPhotoRequestModel photo, IFormFile photoFile);
        List<PhotoModel> GetAll(int categoryId = 0);
        void Delete(int photoId);
    }
}
