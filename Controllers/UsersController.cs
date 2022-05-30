using Auth.Attributes;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // [RequireAuth]
        [HttpGet("permissions")]
        public async Task<IActionResult> PermissionAsync([FromQuery]string userId)
        {
            if (!HttpContext.Items.ContainsKey("user")) return Utils.NORIP();
            AuthDbContext db = new();
            var userRoles = db.Roles.Where(x => x.Userid == userId);
            var userPermissions = from role in userRoles.ToList()
                let permission = (from p in db.Permissions.Where(x => x.Roleid == role.Roleid)
                    select p.Label)
                select permission;
            return new JsonResult(new {userRoles, userPermissions});
        }
        

        [RequireAuth]
        [HttpPost("new")]
        public async Task<IActionResult> NewUsersAsync([FromBody]NewUserRequestModel m)
        {
            AuthDbContext db = new();
            string prefix = $"gbsw{m.Cardinal}";
            var prefixes = m.Users.Select((x, i) => $"{prefix}{i:D3}");
            GeneralResponseModel response = new ();
            if (await db.Users.AnyAsync(x => prefixes.Contains(x.Userid)))
            {
                response.Message = "Please select another cardinal number.";
                response.Success = false;
                return new JsonResult(response);
            }

            var usersToInsert = m.Users.Select((user, i) =>
            {
                var salt = Utils.GenerateRandomSalt(5);
                var id = Utils.SHA512(salt+prefixes.ElementAt(i));
                return new User()
                {
                    // Cardinal = m.Cardinal, Depid = m.DepId, Name = user.Name, Password = id, Phone = user.Phone,
                    // Roles = 0, Salt = salt, Userid = id
                };
            });
            await db.Users.AddRangeAsync(usersToInsert);
            await db.SaveChangesAsync();
            response.Data = usersToInsert;
            return new JsonResult(response);
        }
    }
}
