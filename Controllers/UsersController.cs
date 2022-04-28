using Auth.Attributes;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [RequireAuth]
        [HttpGet("permissions")]
        public async Task<IActionResult> PermissionAsync()
        {
            Console.WriteLine((uint)1 << 31);
            if (!HttpContext.Items.ContainsKey("user")) return Utils.NORIP();
            AuthDbContext db = new();
            var fucking_user_role = db.Users.FirstOrDefault().Roles;
            var r = db.Roles.ToList().Where(r => (fucking_user_role & (1 << r.Roleid)) > 0);
            return new JsonResult(r.SingleOrDefault());
            // return 
        }
        

        [RequireAuth]
        [HttpPost("new")]
        public async Task<IActionResult> NewUsersAsync([FromBody]IEnumerable<NewUserRequestModel>m)
        {
            return new JsonResult(m);
        }

    }
}
