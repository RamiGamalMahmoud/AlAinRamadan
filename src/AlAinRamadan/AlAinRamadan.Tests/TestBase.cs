namespace AlAinRamadan.Tests
{
    public abstract class TestBase : IClassFixture<Services>
    {
        protected readonly Services _services;

        protected TestBase(Services services)
        {
            _services = services;
        }
    }
}
