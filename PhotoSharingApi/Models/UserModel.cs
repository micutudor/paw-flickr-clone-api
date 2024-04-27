namespace PhotoSharingApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Is_Moderator { get; set; }
    }
}
