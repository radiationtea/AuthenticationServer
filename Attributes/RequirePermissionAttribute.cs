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
            
            IEnumerable<string> permissions = (from r in db.Roles.Where(x => x.Userid == user.Userid).ToList()
                let p = from p1 in db.Permissions.Where(x => x.Roleid == r.Roleid)
                    select p1.Label
                select p).SelectMany(x=> x).Distinct();
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
