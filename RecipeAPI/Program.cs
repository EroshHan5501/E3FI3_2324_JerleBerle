using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using RecipeAPI.Authentication;
using RecipeAPI.Authentication.Middleware;
using RecipeAPI.Database;
using RecipeAPI.Exceptions;
using System.Text.Json.Serialization;


DBUserHandler userHandler = new DBUserHandler();
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.AddDbContext<AppDbContext>(
	opt => opt.UseMySql(userHandler.dbConnectionString, ServerVersion.AutoDetect(userHandler.dbConnectionString)));

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
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
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
	app.UseSwagger();
	app.UseSwaggerUI();
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
