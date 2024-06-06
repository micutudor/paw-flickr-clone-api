using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace PhotoSharingApi.DAL.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext dbContext) : base(dbContext) { }

        public List<Comment> GetAllCommentsByPhotoId(int photoId)
        {
            return _dbSet
                .Include(comment => comment.User)
                .Where(comment => comment.photo_id == photoId)
                .OrderByDescending(comment => comment.comment_id)
                .ToList();
        }
    }
}
