using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Photos;
using PhotoSharingApi.Services.Interfaces;

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
        public async Task Add([FromForm] AddPhotoRequestModel photoModel, IFormFile photoFile)
        {
            await _photoService.Add(photoModel, photoFile);
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

        [HttpDelete("[action]")]
        public async Task Delete(int photoId)
        {
            await _photoService.Delete(photoId);
        }
    }
}
