using System.Security.Cryptography;
using Auth.Database.Models;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Attributes
{
    public class RequirePermissionAttribute : ActionFilterAttribute
    {
        public string Permission;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var db = new AuthDbContext();
            var user = (User)context.HttpContext.Items["user"];
            var permissions = (from r in db.Roles.Where(x => x.Userid == user.Userid).ToList()
                let p = from p1 in db.Permissions.Where(x => x.Roleid == r.Roleid)
                    select p1.Label
                select p).SelectMany(x=> x).Distinct();
            if (!permissions.Contains(Permission))
            {
                var m = new GeneralResponseModel();
                m.Success = false;
                m.Message = "insufficient permission.";
                context.Result = new JsonResult(m);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
