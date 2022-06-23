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
    public class DepartsController : ControllerBase
    {
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_DEPARTS)]
        [HttpPost]
        public async Task<IActionResult> NewDepartAsync([FromBody]NewDepartRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            await db.AddAsync(new Depart() { Desc = m.Desc });
            await db.SaveChangesAsync();
            return new JsonResult(response);
        }

        [RequireAuth]
        [HttpGet]
        public async Task<IActionResult> GetDepartAsync([FromQuery] int depid)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            Depart? dep = await db.Departs.SingleOrDefaultAsync(x => x.Depid == depid);

            if (dep == null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            response.Data = dep;
            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_DEPARTS)]
        [HttpPut]
        public async Task<IActionResult> ModifyDepartAsync([FromBody]DepartModifyRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            Depart? dep = await db.Departs.SingleOrDefaultAsync(x => x.Depid == m.DepId);
            if (dep == null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            dep.Desc = m.desc;
            db.Departs.Update(dep);
            await db.SaveChangesAsync();

            return new JsonResult(response);
        }

        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_DEPARTS)]
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartAsync([FromQuery]int depid)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            Depart? dep = await db.Departs.SingleOrDefaultAsync(x => x.Depid == depid);
            if (dep == null)
            {
                response.Success = false;
                response.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(response);
            }

            db.Departs.Remove(dep);
            await db.SaveChangesAsync();

            return new JsonResult(response);
        }
    }
}
