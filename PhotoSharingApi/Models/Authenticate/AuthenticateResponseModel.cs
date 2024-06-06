namespace PhotoSharingApi.Models.Authenticate
{
    public class AuthenticateResponseModel
    {
        public bool Successfull { get; set; }
        public string? Error { get; set; }
        public string? SessionJWT { get; set; }
    }
}
