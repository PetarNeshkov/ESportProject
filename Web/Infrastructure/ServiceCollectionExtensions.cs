using E_SportManager.Service.Data;
using E_SportManager.Service.Data.Players;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,IConfiguration configuration)
        {
            services
                .AddSingleton(configuration)
                .AddScoped<IPlayerService, PlayerService>();

            return services;
        }
    }
}
