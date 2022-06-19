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
    public class DepartController : ControllerBase
    {
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_DEPARTS)]
        [HttpPost("new")]
        public async Task<IActionResult> NewDepartAsync([FromBody]NewDepartRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();

            await db.AddAsync(new Depart() { Desc = m.Desc });
            await db.SaveChangesAsync();
            return new JsonResult(response);
        }
    }
}
