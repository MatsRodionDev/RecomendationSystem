using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Index.HPRtree;
using RecomandationSystem.Application.Models;

namespace RecomandationSystem.Application
{
    public sealed class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IConfiguration configuration) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)), o => o.UseVector());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("vector");

            base.OnModelCreating(modelBuilder);
        }
    }
}
