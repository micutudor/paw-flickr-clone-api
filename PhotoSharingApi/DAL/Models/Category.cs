using Microsoft.EntityFrameworkCore;

namespace PhotoSharingApi.DAL.Models
{
    [PrimaryKey("category_id")]
    public class Category
    {
        public int category_id {  get; set; }
        public string name { get; set; }

        public List<PhotoCategory> PhotoCategories { get; set; }
    }
}
