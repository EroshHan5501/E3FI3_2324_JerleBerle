using Microsoft.AspNetCore.Server.Kestrel.Core;

using RecipeApi.Database;
using RecipeApi.Middlewares;

using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.AddDbContext<RecipeDbContext>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddLogging();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

app.UseMiddleware<ExceptionMiddlware>();

app.UseStaticFiles();
app.UseDefaultFiles();

app.MapDefaultControllerRoute();

app.Run();
