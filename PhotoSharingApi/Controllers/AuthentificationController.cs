using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.DAL;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : Controller
    {
        private readonly IAuthentificationService _authentificationService;

        public AuthentificationController(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        [HttpPost("[action]")]
        public IActionResult Register(UserModel user)
        {
            _authentificationService.Register(user);
            return Ok("Registration successful");
        }

        [HttpPost("[action]")]
        public IActionResult Login(string username, string password)
        {
            var token = _authentificationService.Login(username, password);
            if (token != null)
            {
                return Ok(new { token });
            }
            return Unauthorized("Invalid username or password");
        }

        [HttpPost("[action]")]
        public IActionResult Logout()
        {
            //sterge token jwt
            return Ok("Logout successful");
        }
    }
}
