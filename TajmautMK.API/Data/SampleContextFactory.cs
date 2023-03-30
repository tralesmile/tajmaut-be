using Microsoft.EntityFrameworkCore.Design;
using tajmautAPI.Data;

namespace TajmautMK.API.Data
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<tajmautDataContext>
    {
        public tajmautDataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<tajmautDataContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("TajmautMK.API"));

            return new tajmautDataContext(builder.Options);
        }
    }
}
