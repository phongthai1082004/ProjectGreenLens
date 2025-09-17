using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjectGreenLens.Infrastructure.dbContext
{
    public class GreenLensDbContextFactory : IDesignTimeDbContextFactory<GreenLensDbContext>
    {
        public GreenLensDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("secretsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<GreenLensDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new GreenLensDbContext(optionsBuilder.Options);
        }
    }
}
