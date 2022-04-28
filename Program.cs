using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var conf = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json").Build();
builder.WebHost.UseUrls(conf.GetValue<string>("Host"));
builder.Services.AddControllers().AddJsonOptions(x=> x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.Use(JWTMiddleware.InvokeAsync);
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

//handles 404
app.UseStatusCodePages(async (ctx) =>
{
    if (ctx.HttpContext.Response.StatusCode != 404)
    {
        await ctx.Next(ctx.HttpContext);
        return;
    }
    ctx.HttpContext.Response.Redirect("https://magicalmirai.com");
});
app.Run();