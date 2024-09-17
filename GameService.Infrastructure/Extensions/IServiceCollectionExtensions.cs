using GameService.Infrastructure.Repositories.Implementations;
using GameService.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAchievementRepository, AchievementRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IGameDetailRepository, GameDetailRepository>();
            services.AddScoped<IIgnoredSteamGameRepository, IgnoredSteamGameRepository>();
            services.AddScoped<IPlatformRepository, PlatformRepository>();
        }
    }
}
