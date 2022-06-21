using Auth.Constants;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Attributes
{
    public class RequirePermissionAttribute : ActionFilterAttribute
    {
        public string Permission;
        public int Order = 2;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AuthDbContext db = new ();
            User? user = context.HttpContext.GetUserFromContext();

            IEnumerable<string> permissions = user.GetPermissions();
            if (!permissions.Contains(Permission))
            {
                GeneralResponseModel m = new()
                {
                    Success = false,
                    Code = ResponseCode.FORBIDDEN
                };
                context.Result = new JsonResult(m);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
