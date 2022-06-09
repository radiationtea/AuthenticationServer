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
                var id = Utils.SHA512(prefixes.ElementAt(i));
                return new User()
                {
                    Cardinal = m.Cardinal, Depid = m.DepId, Name = user.Name, Password = id, Phone = user.Phone, Salt = string.Empty, Userid = id
                };
            });
            await db.Users.AddRangeAsync(usersToInsert);
            await db.SaveChangesAsync();
            response.Data = usersToInsert;
            return new JsonResult(response);
        }

#region USER ROLE
        [RequireAuth]
        [RequirePermission(Permission = Permissions.MANAGE_USERS)]
        [HttpPut]
        [Route("modify")]
        public async Task ModifyPutAsync([FromBody]UserModifyRequestModel m) //todo do something
        {

        }
#endregion
    }
}
