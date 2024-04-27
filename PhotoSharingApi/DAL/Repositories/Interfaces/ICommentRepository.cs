using PhotoSharingApi.DAL.Models;
using System.Runtime.InteropServices;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task Create(Comment comment);

        Task<Comment> GetById(int commentId);

        Task Update(Comment comment);

        Task Delete(int commentId);
    }
}
