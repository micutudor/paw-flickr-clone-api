using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoSharingApi.DAL.Models
{
    public class PhotoCategory
    {
        public int category_id { get; set; }
        public Category Category { get; set; }

        public int photo_id { get; set; }
        public Photo Photo { get; set; }
    }
}
