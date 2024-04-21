using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSharingApi.DAL.Models
{
    public class PhotoAlbum
    {
        public int album_id { get; set; }
        public Album Album { get; set; }

        public int photo_id { get; set; }
        public Photo Photo { get; set; }
    }
}
