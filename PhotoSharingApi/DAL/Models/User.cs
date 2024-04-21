using Microsoft.EntityFrameworkCore;

namespace PhotoSharingApi.DAL.Models
{
    [PrimaryKey("user_id")]
    public class User
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int is_moderator { get; set; }

        public List<Album> Albums { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
