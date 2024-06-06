using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task Create(CreateAlbumRequestModel newAlbum)
        {
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);

            await _albumService.Create(userID, newAlbum.Name);
        }

        [HttpGet("[action]")]
        [Authorize]
        public ActionResult<List<UserAlbumsResponseModel>> GetAll()
        {
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);

            return Ok(_albumService.GetUserAlbums(userID));
        }
    }
}
