using System.Text.Json.Serialization;
using Auth.Attributes;
using Auth.Database;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var conf = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json").Build();
JWTHandler.Secret = conf.GetValue<string>("JwtSecret");

builder.WebHost.UseUrls(conf.GetValue<string>("Host"));
builder.Services.AddControllers().AddJsonOptions(x=> x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(x => x.AssumeDefaultVersionWhenUnspecified = true);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



builder.Services.AddScoped<RequireAuthAttribute>();
builder.Services.AddScoped<RequirePermissionAttribute>();

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