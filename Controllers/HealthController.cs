using System.Diagnostics;
using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(new GeneralResponseModel
                { Success = true, Data = new { TotalMemoryUsage = Process.GetCurrentProcess().PagedMemorySize64 } });
        }
    }
}
