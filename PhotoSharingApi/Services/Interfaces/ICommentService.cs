using PhotoSharingApi.Models.Comments;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface ICommentService
    {
        List<CommentResponseModel> GetAllPhotoComments(int photoId);
        Task Create(int authorID, CreateCommentRequestModel comment);
        Task SetStatus(int requesterID, SetCommentStatusModel commentStatus);
        Task Delete(int commentId);

    }
}
