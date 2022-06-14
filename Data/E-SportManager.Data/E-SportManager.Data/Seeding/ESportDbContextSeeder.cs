
using E_SportManager.Data;
using E_SportManager.Data.Seeding;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ESportDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ESportDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext==null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider==null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>()
            {
                new AdministratorSeeder(),
                new PlayerSeeder(),
                new TeamSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
