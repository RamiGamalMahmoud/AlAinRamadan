using AlAinRamadan.Core.Abstraction;

namespace AlAinRamadan.Tests
{
    internal class TestConnectionStringFactory : IConnectionStringFactory
    {
        public string GetConnectionString() => "DataSource=:memory:";
    }
}
