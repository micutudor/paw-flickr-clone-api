using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models;
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
            await _userService.Create(user);
        }

        [HttpGet("[action]")]
        public async Task<string> GetUsernameById(int userid)
        {
            return await _userService.GetUsernameById(userid);
        }
    }
}
