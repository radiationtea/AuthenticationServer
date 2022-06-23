using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Auth.Constants;
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

        public static string GenerateRandomSalt(int length=5)
        {
            StringBuilder builder = new (length);
            Random random = new ();
            for (int i = 0; i < length; i++)
            {
                builder.Append((char)random.Next(minJpnCharCode, maxJpnCharCode));
            }
            return builder.ToString();
        }
        public static EntityEntry<T> AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            bool exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any(x => x == entity);
            return !exists ? dbSet.Add(entity) : null;
        }

        public static EntityEntry<T> RemoveIfExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            bool exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any(x => x == entity);
            return exists ? dbSet.Remove(entity) : null;
        }

        public static async Task<int> GetLastUserNumber(uint cardinal)
        {
            try
            {
                string ident = "gbsw" + cardinal;
                AuthDbContext db = new();
                User? last = db.Users.Where(x => x.Cardinal == cardinal && x.Userid.StartsWith(ident)).ToList().OrderByDescending(x=> x.Userid).FirstOrDefault();
                if (last == null) return 0;
                return GetNumberFromUserId(last.Userid.Remove(0, ident.Length));
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
                Role? role = await db.Roles.OrderByDescending(x => x.Roleid).FirstOrDefaultAsync();
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

        public static User? GetUserFromContext(this HttpContext ctx) => (User?)ctx.Items["user"];

        public static IEnumerable<string> GetPermissions(this User user)
        {
            AuthDbContext db = new();
            IEnumerable<string> permissions = (from r in db.Roles.Where(x => x.Userid == user.Userid).ToList()
                let p = from p1 in db.Permissions.Where(x => x.Roleid == r.Roleid)
                    select p1.Label
                select p).SelectMany(x => x).Distinct();
            return permissions;
        }

        public static IEnumerable<string> GetPermissions(string userId)
        {
            AuthDbContext db = new();
            User? user = db.Users.SingleOrDefault(x => x.Userid == userId);
            if (user == null) return new string[] { };
            IEnumerable<string> permissions = (from r in db.Roles.Where(x => x.Userid == user.Userid).ToList()
                let p = from p1 in db.Permissions.Where(x => x.Roleid == r.Roleid)
                    select p1.Label
                select p).SelectMany(x => x).Distinct();
            return permissions;
        }

        public static bool IsAboveThanMe(this User me, User dest)
        {
            IEnumerable<string> mePerms = me.GetPermissions();
            IEnumerable<string> destPerms = dest.GetPermissions();

            if (mePerms.Contains(Permissions.ADMINISTRATOR) && destPerms.Contains(Permissions.ADMINISTRATOR)) return true;
            if (mePerms.Contains(Permissions.ADMINISTRATOR)) return false;
            if (destPerms.Contains(Permissions.ADMINISTRATOR)) return true;

            if (mePerms.Contains(Permissions.TEACHER) && destPerms.Contains(Permissions.TEACHER)) return true;
            if (mePerms.Contains(Permissions.TEACHER)) return false;
            if (destPerms.Contains(Permissions.TEACHER)) return true;

            return true;

        }
    }
}
