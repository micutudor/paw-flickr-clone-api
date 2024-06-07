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
        private readonly IPhotoRepository _photoRepository;

        public CommentService(IMapper mapper, ICommentRepository commentRepository, IPhotoRepository photoRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _photoRepository = photoRepository;
        }

        public List<CommentResponseModel> GetAllPhotoComments(int photoId) => 
            _mapper.Map<List<CommentResponseModel>>(_commentRepository.GetAllCommentsByPhotoId(photoId));

        public async Task Create(int authorID, CreateCommentRequestModel comment)
        {
            var photoAuthorId = _photoRepository.GetById(comment.PhotoId).user_id;

            var newComment = new Comment
            {
                user_id = authorID, // To be took from middleware
                photo_id = comment.PhotoId,
                comment = comment.Comment,
                commented_at = DateTime.Now,
                status = authorID == photoAuthorId ? (int) CommentStatus.Approved : (int)CommentStatus.WaitingForApproval
               
            };

            await _commentRepository.Add(newComment);
        }

        public async Task SetStatus(int requesterID, SetCommentStatusModel commentStatus)
        {
            var photos = _photoRepository.GetAll().Where(item => item.user_id == requesterID).ToList();

            foreach (var photo in photos)
            {
                var photoId = photo.photo_id;

                if (_commentRepository.GetAllCommentsByPhotoId(photoId).Where(item => item.comment_id == commentStatus.CommentId).Count() == 1)
                {
                    await _commentRepository.Update(item => item.comment_id == commentStatus.CommentId && item.photo_id == photoId, item => item.status = (int)commentStatus.Status);
                    break;
                }
            }
        }

        public async Task Delete(int commentId) => await _commentRepository.Delete(item => item.comment_id == commentId);
    }
}
