using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models.Photos;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IPhotoService
    {
        Task Add(int authorID, AddPhotoRequestModel photo, IFormFile photoFile);
        List<PhotoModel> GetAll(int categoryId = 0);
        Task Delete(int photoId);
        PhotoModel GetPhotoById(int photoId);
        List<PhotoModel> GetPhotoByTitle(string titlePart);
        IsPhotoAuthorResponseModel CheckIfItsAuthor(int userID, int photoId);
    }
}
