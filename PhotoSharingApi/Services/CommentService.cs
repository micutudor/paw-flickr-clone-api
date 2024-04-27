using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;

        private readonly IBaseRepository<Comment> _baseRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentService(IMapper mapper, IBaseRepository<Comment> baseRepository, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
            _commentRepository = commentRepository;
        }

        public List<CommentModel> GetAll() => _mapper.Map<List<CommentModel>>(_baseRepository.GetAll());

        public async Task CreateComment(CommentModel comment)
        {
            var newComment = new Comment
            {
                comment_id = comment.Id,
                user_id = comment.User_Id,
                photo_id = comment.Photo_Id,
                comment = comment.Comment,
                commented_at = comment.Commented_At,
                status = comment.Status
               
            };

            await _commentRepository.Create(newComment);
        }

        public async Task UpdateComment(CommentModel comment)
        {
            var commentToUpdate = new Comment
            {
                comment_id = comment.Id,
                user_id = comment.User_Id,
                photo_id = comment.Photo_Id,
                comment = comment.Comment,
                commented_at = comment.Commented_At,
                status = comment.Status
            };

            if(comment.Status == 2)
                await _commentRepository.Delete(comment.Id);
            else
                await _commentRepository.Update(commentToUpdate);

        }

        public async Task DeleteComment(int commentId)
        {
            await _commentRepository.Delete(commentId);
        }
    }
}
