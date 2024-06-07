using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Photos;
using PhotoSharingApi.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;

namespace PhotoSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<AddPhotoResponseModel>> Add([FromForm] AddPhotoRequestModel photoModel, IFormFile photoFile)
        {
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);

            if (string.IsNullOrWhiteSpace(photoModel.Title))
            {
                return BadRequest(new AddPhotoResponseModel
                {
                    Successfull = false,
                    Error = "The title is empty!"
                });
            }
            else if (string.IsNullOrWhiteSpace(photoModel.Description))
            {
                return BadRequest(new AddPhotoResponseModel
                {
                    Successfull = false,
                    Error = "The description is empty!"
                });
            }
            else if (photoModel.Categories.Count > 0)
            {
                return BadRequest(new AddPhotoResponseModel
                {
                    Successfull = false,
                    Error = "You must select a category!"
                });
            }
            else if (photoModel.Album > 0)
            {
                return BadRequest(new AddPhotoResponseModel
                {
                    Successfull = false,
                    Error = "You must select an album!"
                });
            }
           
            await _photoService.Add(userID, photoModel, photoFile);

            return Ok(new AddPhotoResponseModel
            {
                Successfull = true,
                Error = null
            });
        }

        [HttpGet("[action]")]
        public ActionResult<List<PhotoModel>> GetAll(int categoryId = 0)
        {
            return Ok(_photoService.GetAll(categoryId));
        }

        [HttpGet("[action]")]
        public ActionResult<PhotoModel> GetById(int photoId)
        {
            return Ok(_photoService.GetPhotoById(photoId));
        }

        [HttpGet("[action]")]
        public ActionResult<List<PhotoModel>> GetByTitle(string titlePart)
        {
            return Ok(_photoService.GetPhotoByTitle(titlePart));
        }

        [HttpGet("[action]")]
        [Authorize]
        public ActionResult<IsPhotoAuthorResponseModel> IsAuthor(int photoId)
        {
            int userID = int.Parse(HttpContext.Items["UserID"]?.ToString()!);

            return Ok(_photoService.CheckIfItsAuthor(userID, photoId));
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Moderator")]
        public async Task Delete(int photoId)
        {
            await _photoService.Delete(photoId);
        }
    }
}
