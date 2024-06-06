using PhotoSharingApi.Models.Comments;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface ICommentService
    {
        List<CommentResponseModel> GetAllPhotoComments(int photoId);
        Task Create(CreateCommentRequestModel comment);
        Task SetStatus(SetCommentStatusModel commentStatus);
        Task Delete(int commentId);

    }
}
