using System.Net;
using Auth.Attributes;
using Auth.Constants;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}/@[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        [RequireAuth]
        [HttpGet]
        public async Task<IActionResult> GetMeAsync()
        {
            GeneralResponseModel response = new();

            User user = HttpContext.GetUserFromContext();
            response.Data = new
            {
                CurrentUser = user,
                Permissions = user.GetPermissions(),
                Depart = user.Dep
            };

            return new JsonResult(response);
        }

        [RequireAuth]
        [HttpPut]
        public async Task<IActionResult> ModifyMeAsync([FromBody]MeModifyRequestModel m)
        {
            AuthDbContext db = new();
            GeneralResponseModel response = new();
            User? user = await db.Users.SingleOrDefaultAsync(x => x.Userid == HttpContext.GetUserFromContext().Userid); //for newest data
            if (m.NewPassword != null)
            {
                if (user.Password != Utils.SHA512(user.Salt+m.OldPassword))
                {
                    response.Code = ResponseCode.INCORRECT_PW;
                    response.Success = false;
                    return new JsonResult(response);
                }

                string newsalt = Utils.GenerateRandomSalt();
                user.Password = Utils.SHA512(newsalt + m.NewPassword);
                user.Salt = newsalt;
                db.Users.Update(user);
            }

            await db.SaveChangesAsync();
            HttpContext.Items.Remove("user");
            HttpContext.Response.Cookies.Delete("SESSION_TOKEN");
            return new JsonResult(response);
        }
    }
}
