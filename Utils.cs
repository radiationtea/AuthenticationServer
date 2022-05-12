using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Auth
{
    public class Utils
    {
        public static IActionResult NORIP()
        {
            return new RedirectResult("https://magicalmirai.com/");
        }

        public static string SHA512(string payload)
        {
            SHA512 sha = System.Security.Cryptography.SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(payload));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        const int minJpnCharCode = 0x4e00;
        const int maxJpnCharCode = 0x9FBF;

        public static string GenerateRandomSalt(int length=10)
        {
            var builder = new StringBuilder(length);
            var random = new Random();
            for (var i = 0; i < length; i++)
            {
                builder.Append((char)random.Next(minJpnCharCode, maxJpnCharCode));
            }
            return builder.ToString();
        }
    }
}
