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
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task Create(UserModel user)
        {
            await _userService.CreateUser(user);
        }

        [HttpGet("[action]")]
        public ActionResult<List<UserModel>> GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("[action]")]
        public async Task<string> GetUsernameById(int userid)
        {
            return await _userService.GetUsernameById(userid);
        }
    }
}
