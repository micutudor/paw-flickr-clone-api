using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services;
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
            await _commentService.CreateComment(comment);
        }

        [HttpGet("[action]")]
        public ActionResult<List<CommentModel>> GetAll()
        {
            return Ok(_commentService.GetAll());
        }

        [HttpPatch("[action]")]
        public async Task Update(CommentModel comment)
        {
            await _commentService.UpdateComment(comment);
        }

        [HttpDelete("[action]")]
        public async Task Delete(int commentId)
        {
            await _commentService.DeleteComment(commentId);
        }
    }
}
