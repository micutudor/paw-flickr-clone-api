namespace PhotoSharingApi.Models.Comments
{
    public class SetCommentStatusModel
    {
        public int CommentId { get; set; }
        public CommentStatus Status { get; set; }
    }

    public enum CommentStatus
    {
        WaitingForApproval,
        Approved,
        NotApproved
    }
}
