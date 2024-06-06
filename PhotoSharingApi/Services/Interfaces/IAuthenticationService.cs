using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Models.Authenticate;
using System.Security.Claims;

namespace PhotoSharingApi.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateResponseModel Login(LogInAccountRequestModel existentAccount);
        Task<AuthenticateResponseModel> Register(CreateAccountRequestModel newAccount);
        ClaimsPrincipal ValidateJWTToken(string token);
    }
}
