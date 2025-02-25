namespace AlAinRamadan.Data
{
    public interface IAppDbContextFactory
    {
        AppDbContext CreateDbContext();
    }
}