namespace PhotoSharingApi.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Photo_Id { get; set; }
        public string Comment { get; set; }
        public DateTime Commented_At { get; set; }
        public int Status { get; set; }
    }
}
