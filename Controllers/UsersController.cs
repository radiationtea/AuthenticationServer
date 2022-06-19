using Auth.Attributes;
using Auth.Constants;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Auth.Controllers
{
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpPost("new")]
        public async Task<IActionResult> NewUsersAsync([FromBody]NewUserRequestModel m)
        {
            AuthDbContext db = new();
            string prefix = $"gbsw{m.Cardinal}";
            var last = await Utils.GetLastUserNumber(m.Cardinal);
            var prefixes = m.Users.Select((_, i) => $"{prefix}{i+1+last:D2}");
            GeneralResponseModel response = new();

            var usersToInsert = m.Users.Select((user, i) =>
            {
                var id = prefixes.ElementAt(i);
                if (db.Users.Any(x => x.Userid == id + user.Name))
                {
                    return null;
                }
                return new User()
                {
                    Cardinal = m.Cardinal,
                    Depid = m.DepId,
                    Name = user.Name,
                    Password = Utils.SHA512(id),
                    Phone = user.Phone,
                    Salt = string.Empty,
                    Userid = id
                };
            }).Where(x=> x != null);
            await db.Users.AddRangeAsync(usersToInsert);
            await db.SaveChangesAsync();
            return new JsonResult(response);
        }

        [ApiVersion("0-dev")]
        [HttpGet]
        public async Task<IActionResult> TestEndPoint()
        {
            var last = await Utils.GetLastUserNumber(0);
            var n = Utils.GetNumberFromUserId("gbsw0151");
            return new JsonResult(new { last, n,n1= n, n2=int.Parse("gbsw0151".Substring(5)) });
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] string userId)
        {
            AuthDbContext db = new ();

            var user = await db.Users.SingleOrDefaultAsync(x => x.Userid == userId);
            GeneralResponseModel response = new();
            
            if (user == null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            db.Users.Remove(user);

            await db.SaveChangesAsync();

            return new JsonResult(response);
        }

        #region USER ROLE
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpPut]
        [Route("modify")]
        public async Task<IActionResult> ModifyUserAsync([FromBody]UserModifyRequestModel m)
        {
            AuthDbContext db = new();
            var user = await db.Users.SingleOrDefaultAsync(x => x.Userid == m.UserId);
            GeneralResponseModel response = new();
            if (user == null)
            {
                response.Code = ResponseCode.NOT_FOUND;
                response.Success = false;
                return new JsonResult(response);
            }

            if (m.ResetPassword)
            {
                user.Salt = string.Empty;
                user.Password = Utils.SHA512(m.UserId);
            }

            if (!string.IsNullOrEmpty(m.Phone))
            {
                user.Phone = m.Phone;
            }

            foreach (var i in m.RolesToAdd)
            {
                var role = await db.Roles.SingleOrDefaultAsync(x => x.Roleid == i && x.Userid == string.Empty);
                if (role == null) continue;
                db.Roles.AddIfNotExists(new Role { Label = role.Label, Roleid = role.Roleid, Userid = m.UserId });
            }

            foreach (var i in m.RolesToRemove)
            {
                var role = await db.Roles.SingleOrDefaultAsync(x => x.Roleid == i && x.Userid == string.Empty);
                if (role == null) continue;
                db.Roles.RemoveIfExists(new Role { Label = role.Label, Roleid = role.Roleid, Userid = m.UserId });
            }

            await db.SaveChangesAsync();

            return new JsonResult(response);
        }
#endregion
    }
}
