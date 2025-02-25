using AlAinRamadan.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace AlAinRamadan.Tests
{
    internal class TestAppDbContextFactory : IAppDbContextFactory, IDisposable
    {
        public TestAppDbContextFactory()
        {
            _connection = new SqliteConnection(@"Filename=:memory:");
            _connection.Open();
        }

        public AppDbContext CreateDbContext()
        {
            AppDbContext dbContext= new AppDbContext(new DbContextOptionsBuilder()
                .UseSqlite(_connection)
                .Options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        private readonly SqliteConnection _connection;
    }
}
