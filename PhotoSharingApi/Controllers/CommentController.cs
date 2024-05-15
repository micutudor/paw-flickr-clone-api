using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Comments;
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
        public async Task Create(CommentModel comment)
        {
            await _commentService.Create(comment);
        }

        [HttpGet("[action]")]
        public ActionResult<List<CommentModel>> GetAll()
        {
            return Ok(_commentService.GetAll());
        }

        [HttpPatch("[action]")]
        public async Task SetStatus(SetCommentStatusModel commentStatus)
        {
            await _commentService.SetStatus(commentStatus);
        }

        [HttpDelete("[action]")]
        public async Task Delete(int commentId)
        {
            await _commentService.Delete(commentId);
        }
    }
}
