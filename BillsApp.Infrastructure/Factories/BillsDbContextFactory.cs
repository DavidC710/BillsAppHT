
namespace BillsApp.Infrastructure.Factories
{
    public class BillsDbContextFactory : IBillsDbContextFactory, IDesignTimeDbContextFactory<BillsDbContext>
    {
        public BillsDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BillsDbContext>();

            optionsBuilder.UseSqlServer(config["ConnectionStrings:BillsConn"]);

            return new BillsDbContext(optionsBuilder.Options, new AuditableEntitySaveChangesInterceptor());
        }
    }    
}
