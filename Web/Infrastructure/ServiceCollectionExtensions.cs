using E_SportManager.Data;
using E_SportManager.Service.Data;
using E_SportManager.Service.Data.Players;
using E_SportManager.Service.Data.Teams;
using E_SportManager.Service.Data.Users;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
         this IServiceCollection services,
         IConfiguration configuration)
         => services
             .AddDbContext<ESportDbContext>(options => options
                  .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options=>options
                 .SetIdentityOptions())
             .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<ESportDbContext>();

            return services;
        }

          public static IServiceCollection AddAntiforgeryHeader(this IServiceCollection services)
          => services
              .AddAntiforgery(options => options
                  .HeaderName = "X-CSRF-TOKEN");

        public static IServiceCollection AddControllersWithAutoAntiforgeryTokenAttribute(this IServiceCollection services)
        {    
            services
                    .AddControllersWithViews(options => options
                    .Filters.Add<AutoValidateAntiforgeryTokenAttribute>());

            return services;
        }

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,IConfiguration configuration)
        {
            services
                .AddSingleton(configuration)
                .AddScoped<IPlayerService, PlayerService>()
                .AddScoped<IUserService,UserService>()
                .AddScoped<ITeamService,TeamService>();

            return services;
        }
    }
}
