using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.Models.Albums
{
    public class GetAlbumResponseModel
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
