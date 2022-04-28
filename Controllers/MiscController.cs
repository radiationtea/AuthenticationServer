using Auth.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    public class MiscController : ControllerBase
    {

        [HttpGet]
        [Route("/test")]
        public async Task<IActionResult> TestUser()
        {
            var username = new string(Guid.NewGuid().ToString("N").Take(12).ToArray());
            var password = Utils.SHA256("test");
            User u = new();
            u.Userid = username;
            u.Password = password;

            AuthDbContext ctx = new();
            await ctx.Users.AddAsync(u);
            await ctx.SaveChangesAsync();

            return new JsonResult(new { UserName = username, Password = "test" });
        }
    }
}
