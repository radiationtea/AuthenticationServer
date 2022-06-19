using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Auth.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Auth
{
    public static class Utils
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
        public static EntityEntry<T> AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any(x => x == entity);
            return !exists ? dbSet.Add(entity) : null;
        }

        public static EntityEntry<T> RemoveIfExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any(x => x == entity);
            return exists ? dbSet.Remove(entity) : null;
        }

        public static async Task<int> GetLastUserNumber(uint cardinal)
        {
            try
            {
                AuthDbContext db = new();
                var last = db.Users.Where(x => x.Cardinal == cardinal && x.Userid.StartsWith("gbsw")).ToList().OrderByDescending(x=> GetNumberFromUserId(x.Userid)).FirstOrDefault();
                if (last == null) return 0;
                Console.WriteLine(JsonConvert.SerializeObject(last));
                return GetNumberFromUserId(last.Userid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public static async Task<uint> GetLastRoleNumber()
        {
            try
            {
                AuthDbContext db = new();
                var role = await db.Roles.OrderByDescending(x => x.Roleid).FirstOrDefaultAsync();
                if (role == null) return 0;
                return role.Roleid;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int GetNumberFromUserId(string userId)
        {
            return int.Parse(new Regex(@"\d+").Match(userId).Value);
        }
    }
}
