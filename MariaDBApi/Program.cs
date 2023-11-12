
using MariaDBApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MariaDBApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = "server=localhost;port=3306;database=recipeapp;user=root;password=Gatling762";
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<RecipeContext>(
                opt => opt.UseMySql(connectionString, serverVersion));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}