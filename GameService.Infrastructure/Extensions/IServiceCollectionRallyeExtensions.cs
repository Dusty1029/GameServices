using GameService.Infrastructure.Repositories.Implementations.Rallye;
using GameService.Infrastructure.Repositories.Interfaces.Rallye;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Infrastructure.Extensions
{
    public static class IServiceCollectionRallyeExtensions
    {
        public static void AddRallyeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, PlayerRepository>();
        }
    }
}
