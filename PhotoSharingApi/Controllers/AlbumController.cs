using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;

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
        public async Task<ActionResult<CreateAlbumResponseModel>> Create(CreateAlbumRequestModel newAlbum)
        {
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);


            if (string.IsNullOrWhiteSpace(newAlbum.Name))
            {
                return BadRequest(new CreateAlbumResponseModel
                {
                    Successfull = false,
                    Error = "The name is empty!",
                });
            }

            await _albumService.Create(userID, newAlbum.Name);

            return Ok(new CreateAlbumResponseModel
            {
                Successfull = true,
                Error = null,
            });
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
