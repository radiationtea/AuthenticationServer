using Auth.Attributes;
using Auth.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> NewDepartAsync()
        {

        }
    }
}
