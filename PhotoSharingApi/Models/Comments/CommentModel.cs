namespace PhotoSharingApi.Models.Comments
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set; }
        public string Comment { get; set; }
        public DateTime CommentedAt { get; set; }
        public int Status { get; set; }
    }
}
