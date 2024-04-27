namespace PhotoSharingApi.Models.Photos
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedOn { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PhotoGeolocationModel Geolocation { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
