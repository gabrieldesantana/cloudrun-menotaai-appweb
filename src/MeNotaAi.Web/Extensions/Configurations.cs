using MeNotaAi.Application.Services;
using MeNotaAi.Domain.Interfaces.Repositories;
using MeNotaAi.Domain.Interfaces.Services;
using MeNotaAi.Infrastructure.Persistence.Context;
using MeNotaAi.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MeNotaAi.Web.Extensions
{
    public static class Configurations
    {

        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IVisitanteService, VisitanteService>();
        }

        public static void RegisterInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                ?? configuration.GetConnectionString("DefaultConnection");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
            services.AddScoped<IVisitanteRepositoy, VisitanteRepository>();
        }
    }
}
