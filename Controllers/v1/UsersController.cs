using Auth.Attributes;
using Auth.Constants;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Auth.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpPost]
        public async Task<IActionResult> NewUsersAsync([FromBody]NewUserRequestModel m)
        {
            AuthDbContext db = new();
            string prefix = $"gbsw{m.Cardinal}";
            int last = await Utils.GetLastUserNumber(m.Cardinal);
            IEnumerable<string> prefixes = m.Users.Select((_, i) => $"{prefix}{i+1+last:D2}");
            GeneralResponseModel response = new();

            if (m.Users.Any(x=> x.Name.Length > 4))
            {
                response.Success = false;
                response.Code = ResponseCode.BAD_REQUEST;
                return new JsonResult(response);
            }

            IEnumerable<User?> usersToInsert = m.Users.Select((user, i) =>
            {
                string id = prefixes.ElementAt(i);
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

            response.Data = usersToInsert;
            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync([FromQuery] string userid)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            User? user = await db.Users.SingleOrDefaultAsync(x => x.Userid == userid);
            if (user == null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            response.Data = user;

            return new JsonResult(response);
        }
        
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] string userId)
        {
            AuthDbContext db = new ();
            GeneralResponseModel response = new();

            User? user = await db.Users.SingleOrDefaultAsync(x => x.Userid == userId);
            
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

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpPut]
        public async Task<IActionResult> ModifyUserAsync([FromBody] UserModifyRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            User? user = await db.Users.SingleOrDefaultAsync(x => x.Userid == m.UserId);
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

            foreach (int i in m.RolesToAdd)
            {
                Role? role = await db.Roles.SingleOrDefaultAsync(x => x.Roleid == i && x.Userid == string.Empty);
                if (role == null) continue;
                db.Roles.AddIfNotExists(new Role { Label = role.Label, Roleid = role.Roleid, Userid = m.UserId });
            }

            foreach (int i in m.RolesToRemove)
            {
                Role? role = await db.Roles.SingleOrDefaultAsync(x => x.Roleid == i && x.Userid == string.Empty);
                if (role == null) continue;
                db.Roles.RemoveIfExists(new Role { Label = role.Label, Roleid = role.Roleid, Userid = m.UserId });
            }

            await db.SaveChangesAsync();

            return new JsonResult(response);
        }
    }
}
