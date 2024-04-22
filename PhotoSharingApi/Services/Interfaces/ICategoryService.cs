using PhotoSharingApi.Models;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryModel> GetAll();
    }
}
