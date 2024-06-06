namespace PhotoSharingApi.Models.Comments
{
    public class CommentResponseModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
