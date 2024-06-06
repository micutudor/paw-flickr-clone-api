using System.Security.Cryptography;
using System.Text;

namespace PhotoSharingApi.Helpers
{
    public static class HashPassword
    {
        public static string Hash(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
