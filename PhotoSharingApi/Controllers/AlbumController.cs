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
        public async Task Create(CreateAlbumRequestModel album)
        {
            await _albumService.Create(album);
        }

        [HttpGet("[action]")]
        public ActionResult<List<GetAlbumResponseModel>> Get(int albumId)
        {
            return Ok(_albumService.Get(albumId));
        }

        [HttpGet("[action]")]
        public ActionResult<List<UserAlbumsResponseModel>> GetUserAll(int userId)
        {
            return Ok(_albumService.GetUserAlbums(userId));
        }
    }
}
