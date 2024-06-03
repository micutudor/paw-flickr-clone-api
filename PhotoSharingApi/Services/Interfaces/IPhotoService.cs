using PhotoSharingApi.Models.Photos;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IPhotoService
    {
        Task Add(AddPhotoRequestModel photo, IFormFile photoFile);
        List<PhotoModel> GetAll(int categoryId = 0);
        Task Delete(int photoId);
        PhotoModel GetPhotoById(int photoId);
        List<PhotoModel> GetPhotoByTitle(string titlePart);
    }
}
