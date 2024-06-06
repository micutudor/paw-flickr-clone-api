using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.Models.Albums
{
    public class UserAlbumsResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AlbumPhotoResponseModel> Photos { get; set; }
    }
}
