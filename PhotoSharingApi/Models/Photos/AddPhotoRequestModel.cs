namespace PhotoSharingApi.Models.Photos
{
    public class AddPhotoRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Album { get; set; }
        public PhotoGeolocationModel Geolocation { get; set; }
        public HashSet<int> Categories { get; set; }
    }
}
