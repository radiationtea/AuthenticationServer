using Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/auth/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(new GeneralResponseModel
                { Success = true, Data=new{Test=true} });
        }
    }
}
