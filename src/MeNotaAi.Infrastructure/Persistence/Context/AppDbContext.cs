using MeNotaAi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MeNotaAi.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Visitante> Visitantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                ?? _configuration.GetConnectionString("DefaultConnection");

            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("MeNotaAi.Infrastructure"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
