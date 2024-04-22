using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]")]
        public ActionResult<List<CategoryModel>> GetAll() 
        {
            return Ok(_categoryService.GetAll());
        }
    }
}
