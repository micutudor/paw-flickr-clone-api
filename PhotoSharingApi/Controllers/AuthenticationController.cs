using Microsoft.AspNetCore.Mvc;
using PhotoSharingApi.Models.Authenticate;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthenticateResponseModel>> Register(CreateAccountRequestModel newAccount)
        {
            var result = await _authenticationService.Register(newAccount);

            if (!result.Successfull)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("[action]")]
        public ActionResult<AuthenticateResponseModel> Login(LogInAccountRequestModel existentAccount)
        {
            var result = _authenticationService.Login(existentAccount);

            if (!result.Successfull)
            {
                return Unauthorized(result);
            }
            
            return Ok(result);
        }
    }
}
