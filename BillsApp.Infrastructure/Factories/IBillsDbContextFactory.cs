namespace BillsApp.Infrastructure.Factories
{
    public interface IBillsDbContextFactory
    {
        BillsDbContext CreateDbContext(string[] args);
    }
}
