using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSharingApi.DAL.Models
{
    [PrimaryKey("album_id")]
    public class Album
    {
        public int album_id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }

        public int user_id { get; set; }
        public User User { get; set; }

        public List<PhotoAlbum> PhotoAlbums { get; set; }
    }
}
