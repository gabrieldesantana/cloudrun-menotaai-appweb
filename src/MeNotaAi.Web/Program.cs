using MeNotaAi.Infrastructure.Persistence.Context;
using MeNotaAi.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MeNotaAi.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(opt => opt.Conventions.AddPageRoute("/Visitantes/Index", ""));

            var conn = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.RegisterApplicationServices();
            builder.Services.RegisterInfrastuctureServices(builder.Configuration);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            using var db = scope.ServiceProvider.GetService<AppDbContext>();
            db.Database.Migrate();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            app.UseExceptionHandler("/Error");
            //}
            app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                {
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/Error";
                    await next();
                }
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
