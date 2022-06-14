using E_SportManager.Data;
using E_SportManager.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        //checks whether there are new migrations and apply them when the program starts
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            //Creating a scope where the code will exist
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetRequiredService<ESportDbContext>();

            data.Database.Migrate();

            SeedData(app);

            return app;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scopedService= app.ApplicationServices.CreateScope();

            using var dbContext=scopedService.ServiceProvider.GetRequiredService<ESportDbContext>();

            new ESportDbContextSeeder()
                .SeedAsync(dbContext,scopedService.ServiceProvider)
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
