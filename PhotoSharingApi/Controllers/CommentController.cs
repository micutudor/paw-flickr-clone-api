using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Comments;
using PhotoSharingApi.Models.Enums;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("[action]")]
        public async Task Create(CreateCommentRequestModel comment)
        {
            await _commentService.Create(comment);
        }

        [HttpGet("[action]")]
        public ActionResult<List<CommentResponseModel>> GetPhotoAll(int photoId)
        {
            return Ok(_commentService.GetAllPhotoComments(photoId));
        }

        [HttpPatch("[action]")]
        [Authorize]
        public async Task SetStatus(SetCommentStatusModel commentStatus)
        {
            await _commentService.SetStatus(commentStatus);
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Moderator")]
        public async Task Delete(int commentId)
        {
            await _commentService.Delete(commentId);
        }
    }
}
