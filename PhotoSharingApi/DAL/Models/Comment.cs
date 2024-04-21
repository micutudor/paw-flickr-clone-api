using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSharingApi.DAL.Models
{
    [PrimaryKey("comment_id")]
    public class Comment
    {
        public int comment_id { get; set; }
        public string comment { get; set; }
        public DateTime commented_at { get; set; }
        public int status { get; set; }

        public int user_id { get; set; }
        public User User { get; set; }

        public int photo_id { get; set; }
        public Photo Photo { get; set; }
    }
}
