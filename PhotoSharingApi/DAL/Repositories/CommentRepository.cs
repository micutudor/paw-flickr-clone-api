using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _dbContext;

        public CommentRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Comment> GetById(int commentId)
        {
            return await _dbContext.Comments.FindAsync(commentId);
        }

        public async Task Update(Comment comment)
        {
            _dbContext.Comments.Entry(comment).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int commentId)
        {
            var result = await this.GetById(commentId);

            if (result != null)
            {
                _dbContext.Comments.Remove(result);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
