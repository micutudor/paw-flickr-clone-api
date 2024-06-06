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
        [Authorize]
        public async Task Create(CreateCommentRequestModel comment)
        {
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);

            await _commentService.Create(userID, comment);
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
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);

            await _commentService.SetStatus(userID, commentStatus);
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Moderator")]
        public async Task Delete(int commentId)
        {
            await _commentService.Delete(commentId);
        }
    }
}
