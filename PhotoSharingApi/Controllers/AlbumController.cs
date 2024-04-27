using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Services;
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
        public void Create(CreateAlbumRequestModel album)
        {
            _albumService.Create(album);
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
