namespace PhotoSharingApi.Models.Comments
{
    public class CreateCommentRequestModel
    {
        public int PhotoId { get; set; }
        public string Comment { get; set; }
    }
}
