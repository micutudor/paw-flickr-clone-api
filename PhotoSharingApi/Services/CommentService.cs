using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models.Comments;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;

        private readonly ICommentRepository _commentRepository;

        public CommentService(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public List<CommentResponseModel> GetAllPhotoComments(int photoId) => 
            _mapper.Map<List<CommentResponseModel>>(_commentRepository.GetAllCommentsByPhotoId(photoId));

        public async Task Create(CreateCommentRequestModel comment)
        {
            var newComment = new Comment
            {
                user_id = 1, // To be took from middleware
                photo_id = comment.PhotoId,
                comment = comment.Comment,
                commented_at = DateTime.Now,
                status = (int)CommentStatus.WaitingForApproval
               
            };

            await _commentRepository.Add(newComment);
        }

        public async Task SetStatus(SetCommentStatusModel commentStatus) => 
            await _commentRepository.Update(item => item.comment_id == commentStatus.CommentId, item => item.status = (int)commentStatus.Status);

        public async Task Delete(int commentId) => await _commentRepository.Delete(item => item.comment_id == commentId);
    }
}
