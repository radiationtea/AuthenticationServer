using Auth.Database;
using Auth.Models;

public class JWTMiddleware
{
    public static async Task InvokeAsync(HttpContext ctx, Func<Task> next)
    {
        if (ctx.Request.Cookies.ContainsKey("SESSION_TOKEN"))
        {
            var u = JWTHandler.DecodeJWT(ctx.Request.Cookies["SESSION_TOKEN"]);

            if (u == null)
            {
                Console.WriteLine("Unauthorized Removing User");
                ctx.Items.Remove("user");
            }
            else
            {
                Console.WriteLine("User ok");
                ctx.Items.Add("user", u);
            }
        }
        else ctx.Items.Remove("user");
        await next();
    }
}