using Auth.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}/[controller]")]

    public class MiscController : ControllerBase
    {

    }
}
