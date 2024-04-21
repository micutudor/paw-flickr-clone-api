using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSharingApi.DAL.Models
{
    [PrimaryKey("photo_id")]
    public class Photo
    {
        public int photo_id {  get; set; }
        public DateTime posted_at { get; set; }
        public string path { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string geolocation { get; set; }

        public int user_id { get; set; }
        public User User {  get; set; }
        
        public List<Comment> Comments { get; set; }
        public List<PhotoAlbum> PhotoAlbums { get; set; }
        public List<PhotoCategory> PhotoCategories { get; set; }
    }
}
