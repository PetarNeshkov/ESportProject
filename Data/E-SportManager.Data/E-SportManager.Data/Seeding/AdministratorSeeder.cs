using E_SportManager.Data;
using E_SportManager.Data.Seeding;
using Microsoft.AspNetCore.Identity;

using static E_SportManager.Common.GlobalConstants.Administrator;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AdministratorSeeder : ISeeder
    {
        public async Task SeedAsync(ESportDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var isExistingAdmin = await roleManager.RoleExistsAsync(AdministratorRoleName);

            if (!isExistingAdmin)
            {
                var role = new IdentityRole { Name = AdministratorRoleName };

                var admin = new User
                {
                    UserName = AdministratorUsername,
                    Email = AdministratorEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin,AdministratorPassword);

                await userManager.AddToRoleAsync(admin, role.Name);
            }
        }
    }
}
