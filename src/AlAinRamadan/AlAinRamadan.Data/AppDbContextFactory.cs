using AlAinRamadan.Core.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace AlAinRamadan.Data
{
    public class AppDbContextFactory
    {
        public AppDbContextFactory(IConnectionStringFactory connectionStringFactory)
        {
            _connectionStringFactory = connectionStringFactory;
        }

        public AppDbContext CreateDbContext()
        {
            return new AppDbContext(new DbContextOptionsBuilder()
                .UseSqlite(_connectionStringFactory.GetConnectionString())
                .Options);
        }

        private readonly string _connectionString;
        private readonly IConnectionStringFactory _connectionStringFactory;
    }
}
