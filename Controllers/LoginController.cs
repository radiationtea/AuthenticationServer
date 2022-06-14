using Auth.Constants;
using Auth.Database;
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
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequestModel request)
        {
            AuthDbContext db = new();
            var user = await db.Users.SingleOrDefaultAsync(x => x.Userid == request.UserId);
            GeneralResponseModel m = new();
            if (user == null)
            {
                m.Success = false;
                m.Code = ResponseCode.NOT_FOUND;
                return new JsonResult(m);
            }

            if (user.Password != Utils.SHA512(user.Salt+request.Password))
            {
                m.Success = false;
                m.Code = ResponseCode.INCORRECT_PW;
                return new JsonResult(m);
            }

            string token = await JWTHandler.GenerateJWTAsync(user.Userid);
            m.Success = true;
            m.Data = new { Token = token };
            if (user.Salt == string.Empty)
            {
                m.Success = true;
                m.Code = ResponseCode.CHANGE_PW;
                return new JsonResult(m);
            }
            return new JsonResult(m);
        }
    }
}
