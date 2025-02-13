using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlAinRamadan.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder
                .UseSqlite("Data Source = .\\DB\\al_ain_ramadan.db");

            AppDbContext dbContext = new AppDbContext(builder.Options);
            return dbContext;
        }
    }
}
