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

        public static string SHA256(string payload)
        {
            SHA256 sha = System.Security.Cryptography.SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(payload));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}
