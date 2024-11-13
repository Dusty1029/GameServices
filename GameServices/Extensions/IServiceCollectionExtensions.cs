using CommonV2.Extensions;
using GameService.API.BusinessLogics.Implementations;
using GameService.API.BusinessLogics.Interfaces;
using GameService.Infrastructure.Extensions;

namespace GameService.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddMemoryCacheServices();
            services.AddRepositories();
            services.AddBusinessLogics();
        }
        private static void AddBusinessLogics(this IServiceCollection services)
        {
            services.AddScoped<ICategoryBL, CategoryBL>();
            services.AddScoped<IGameBL, GameBL>();
            services.AddScoped<IAchievementBL, AchievementBL>();
            services.AddScoped<ISteamBL, SteamBL>();
            services.AddScoped<IPlaystationBL, PlaystationBL>();
            services.AddScoped<IPlatformBL, PlatformBL>();
            services.AddScoped<ISerieBL, SerieBL>();
            services.AddScoped<IXboxBL, XboxBL>();
            services.AddScoped<IGoalBL, GoalBL>();
        }

    }
}
