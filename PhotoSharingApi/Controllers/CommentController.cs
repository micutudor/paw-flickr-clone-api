using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Comments;
using PhotoSharingApi.Models.Enums;
using PhotoSharingApi.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;

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
        public async Task<ActionResult<CreateCommentResponseModel>> Create(CreateCommentRequestModel comment)
        {
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);

            if (string.IsNullOrWhiteSpace(comment.Comment))
            {
                return BadRequest(new CreateCommentResponseModel
                {
                    Successfull = false,
                    Error = "The comment is empty!"
                });
            }

            await _commentService.Create(userID, comment);

            return Ok(new CreateCommentResponseModel
            {
                Successfull = true,
                Error = null
            });
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
