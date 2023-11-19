using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;

using RecipeApi.Authentication;
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

builder.Services.AddAuthentication(options =>
{
    options.RequireAuthenticatedSignIn = false;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    string domain = "localhost";

    if (builder.Environment.IsProduction())
    {
        domain = "recipe-app.com";
    }

    options.ClaimsIssuer = "RecipeApp";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(0);
    options.Cookie = new CookieBuilder()
    {
        Name = "sid",
        HttpOnly = true,
        IsEssential = true,
        SameSite = SameSiteMode.Strict,
        Domain = domain
    };

    options.EventsType = typeof(RecipeAuthenticationEvents);
});

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("recipePolicy", config =>
    {
        config.AllowAnyOrigin();
    });
});

builder.Services.AddScoped<RecipeAuthenticationEvents>();
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

app.UseCors("recipePolicy");

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseMiddleware<LoginMiddleware>();
app.UseMiddleware<LogoutMiddleware>();

app.UseAuthentication();

app.UseAuthorization();
app.MapDefaultControllerRoute();

app.MapFallbackToFile("/index.html");

app.Run();
