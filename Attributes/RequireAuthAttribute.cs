using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Attributes
{
    public class RequireAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Items.ContainsKey("user"))
            {
                var m = new GeneralResponseModel();
                m.Success = false;
                m.Message = "Need authenticated.";
                context.Result = new JsonResult(m);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
