using Auth.Constants;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Attributes
{
    public class RequireAuthAttribute : Attribute, IAsyncActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("please1");
            Console.WriteLine(context.HttpContext.Items["user"] is null);
            if (!context.HttpContext.Items.ContainsKey("user") || context.HttpContext.Items["user"] is null)
            {
                var m = new GeneralResponseModel();
                m.Success = false;
                m.Code = ResponseCode.UNAUTHORIZED;
                context.Result = new JsonResult(m);
                return;
            }
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Items.ContainsKey("user") || context.HttpContext.Items["user"] is null)
            {
                var m = new GeneralResponseModel();
                m.Success = false;
                m.Code = ResponseCode.UNAUTHORIZED;
                context.Result = new JsonResult(m);
                context.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            }

            return next();
        }


    }
}
