using AlAinRamadan.Core.Models;
using AlAinRamadan.Data.ModelsConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AlAinRamadan.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IModelsConfigurationMarker).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
