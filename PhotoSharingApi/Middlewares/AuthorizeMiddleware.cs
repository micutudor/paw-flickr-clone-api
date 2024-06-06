using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhotoSharingApi.Services.Interfaces;
using System.Security.Claims;

namespace PhotoSharingApi.Middlewares
{
    public class AuthorizeMiddleware : IMiddleware
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authorizationService;

        public AuthorizeMiddleware(IUserService userService, IAuthenticationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Get the token from the Authorization header
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!token.IsNullOrEmpty())
            {
                try
                {
                    var claimsPrincipal = _authorizationService.ValidateJWTToken(token);
                    var username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
                    var userID = _userService.GetIDByUsername(username);
                    context.Items["UserID"] = userID;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await next(context);
        }
    }
}
