using Auth.Attributes;
using Auth.Constants;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_ROLES)]
        [HttpPost]
        public async Task <IActionResult> NewRoleAsync([FromBody]NewRoleRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();
            Role? role = await db.Roles.SingleOrDefaultAsync(x => x.Label == m.Label);

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
        [RequirePermission(Permission = Permissions.MANAGE_ROLES)]
        [HttpGet]
        public async Task<IActionResult> GetRoleAsync([FromQuery] int roleid)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            Role? role = await db.Roles.SingleOrDefaultAsync(x => x.Roleid == roleid && x.Userid == string.Empty);
            if (role == null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            response.Data = role;

            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpPut]
        public async Task<IActionResult> ModifyPutAsync([FromBody]RoleModifyRequestModel m)
        {
            AuthDbContext db = new ();
            GeneralResponseModel response = new();

            Role? role = await db.Roles.SingleOrDefaultAsync(x => x.Roleid == m.RoleId && x.Userid == string.Empty);
            if (role == null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            if (m.Label != null)
            {
                role.Label = m.Label;
                IEnumerable<Role> roles = db.Roles.Where(x => x.Roleid == role.Roleid);
                foreach (Role role1 in roles)
                {
                    role1.Label = m.Label;
                }
            }

            foreach (string permission in m.PermissionsToAdd)
            {
                db.Permissions.Add(new() { Label = permission, Roleid = role.Roleid });
            }

            foreach (uint i in m.PermissionsToRemove)
            {
                Permission? perm = await db.Permissions.SingleOrDefaultAsync(x => x.Permid == i);
                if (perm == null) continue;
                db.Permissions.Remove(perm);
            }

            await db.SaveChangesAsync();

            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_ROLES)]
        [HttpDelete]
        public async Task<IActionResult> RemoveRoleAsync([FromQuery] uint roleId)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            IEnumerable<Role> roles = db.Roles.Where(x => x.Roleid == roleId);
            if (!roles.Any())
            {
                response.Success = false;
                response.Code = ResponseCode.ROLE_NOT_FOUND;
                return new JsonResult(response);
            }

            IQueryable<Permission> perms = from i in db.Permissions
                where i.Roleid == roleId
                select i;
            
            db.Permissions.RemoveRange(perms);
            db.Roles.RemoveRange(roles);

            await db.SaveChangesAsync();

            return new JsonResult(response);
        }
        
    }
}