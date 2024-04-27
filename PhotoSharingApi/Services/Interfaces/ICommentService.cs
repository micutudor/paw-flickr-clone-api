using PhotoSharingApi.Models;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface ICommentService
    {
        List<CommentModel> GetAll();

        Task CreateComment(CommentModel comment);

        Task UpdateComment(CommentModel comment);

        Task DeleteComment(int commentId);

    }
}
