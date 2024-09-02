using System.Security.Cryptography;
using System.Text;

namespace IGS.Domain.Extensions
{
    public static class HashHelper
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
