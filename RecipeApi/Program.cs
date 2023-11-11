using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;

using RecipeApi.Database;
using RecipeApi.Middlewares;
using RecipeApi.Middlewares.Authentication;

using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.AddDbContext<RecipeDbContext>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ClaimsIssuer = "RecipeApp";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("recipePolicy", config =>
    {
        config.AllowAnyOrigin();
    });
});

builder.Services.AddLogging();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

app.UseMiddleware<ExceptionMiddlware>();

app.UseCookiePolicy(new CookiePolicyOptions()
{
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseCors();

app.UseMiddleware<LoginMiddleware>();
app.UseMiddleware<LogoutMiddleware>();

app.UseAuthentication();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthorization();
app.MapDefaultControllerRoute();

app.Run();
