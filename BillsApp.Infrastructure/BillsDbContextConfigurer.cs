
namespace BillsApp.Infrastructure
{
    public static class BillsDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BillsDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BillsDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }       
    }
}
