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
        [RequirePermission(Permission = Permissions.ADMINISTRATOR)]
        [HttpPost]
        public async Task<IActionResult> NewUserAsync([FromBody]NewUserRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            User? u = await db.Users.SingleOrDefaultAsync(x => x.Userid == m.UserId);
            if (u != null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            u = new();
            u.Userid = m.UserId;
            u.Name = m.Name;
            u.Phone = m.Phone;
            u.Cardinal = 0;
            u.Depid = 0;
            u.Password = Utils.SHA512(m.UserId);
            u.Salt = string.Empty;
            await db.Users.AddAsync(u);
            await db.SaveChangesAsync();

            response.Data = u;
            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.ADMINISTRATOR)]
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
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredUsersAsync([FromQuery]bool excludeStudent = false) // todo request model
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            IEnumerable<User> users = excludeStudent
                ? db.Users.Where(x => !x.Userid.StartsWith("gbsw"))
                : db.Users;
            //
            // if (m.Cardinal.HasValue)
            // {
            //     users = users.Where(x => x.Cardinal == m.Cardinal);
            // }
            //
            // if (m.DepartId.HasValue)
            // {
            //     users = users.Where(x => x.Depid == m.DepartId);
            // }

            response.Data = users;

            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.ADMINISTRATOR)]
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

            if (HttpContext.GetUserFromContext().IsAboveThanMe(user))
            {
                response.Success = false;
                response.Code = ResponseCode.FORBIDDEN;
                return new JsonResult(response);
            }

            db.Users.Remove(user);

            await db.SaveChangesAsync();

            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.ADMINISTRATOR)]
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

            if (HttpContext.GetUserFromContext().IsAboveThanMe(user))
            {
                response.Success = false;
                response.Code = ResponseCode.FORBIDDEN;
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
