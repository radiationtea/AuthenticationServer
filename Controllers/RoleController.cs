using Auth.Attributes;
using Auth.Constants;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers
{
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_ROLES)]
        [HttpPost("new")]
        public async Task <IActionResult> NewRoleAsync([FromBody]NewRoleRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();
            var role = await db.Roles.SingleOrDefaultAsync(x => x.Label == m.Label);

            if (role != null)
            {
                response.Success = false;
                response.Code = ResponseCode.EXIST;
                return new JsonResult(response);
            }

            Role r = new() { Label = m.Label };
            db.Roles.Add(r);
            await db.SaveChangesAsync();

            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpPut("modify")]
        public async Task<IActionResult> ModifyPutAsync([FromBody]RoleModifyRequestModel m)
        {
            AuthDbContext db = new ();
            GeneralResponseModel response = new();

            var role = await db.Roles.SingleOrDefaultAsync(x => x.Roleid == m.RoleId);
            if (role == null)
            {
                response.Success = false;
                response.Code = ResponseCode.ROLE_NOT_FOUND;
                return new JsonResult(response);
            }

            if (m.Label != null) role.Label = m.Label;

            foreach (var permission in m.PermissionsToAdd)
            {
                db.Permissions.Add(new() { Label = permission, Roleid = role.Roleid });
            }

            foreach (var i in m.PermissionsToRemove)
            {
                var perm = await db.Permissions.SingleOrDefaultAsync(x => x.Permid == i);
                db.Permissions.Remove(perm);
            }

            await db.SaveChangesAsync();

            return new JsonResult(response);
        }
        
    }
}