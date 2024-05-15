using PhotoSharingApi.Models.Comments;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface ICommentService
    {
        List<CommentModel> GetAll();
        Task Create(CommentModel comment);
        Task SetStatus(SetCommentStatusModel commentStatus);
        Task Delete(int commentId);

    }
}
