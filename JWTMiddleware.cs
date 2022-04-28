using Auth.Database;
using Auth.Models;

public class JWTMiddleware
{
    public static async Task InvokeAsync(HttpContext ctx, Func<Task> next)
    {
        // if (!ctx.Request.Cookies.ContainsKey("SESSION_TOKEN"))
        // {
        //     GeneralResponseModel m = new();
        //     m.Success = false;
        //     m.Message = "No SESSION_TOKEN Provided.";
        //     await ctx.Response.WriteAsJsonAsync(m);
        //     return;
        // }
        if (ctx.Request.Cookies.ContainsKey("SESSION_TOKEN"))
        {
            var u = JWTHandler.DecodeJWT(ctx.Request.Cookies["SESSION_TOKEN"]);
            ctx.Items.Add("user", u);
        }
        await next();
    }
}