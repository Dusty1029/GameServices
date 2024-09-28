using GameService.API.BusinessLogics.Implementations;
using GameService.API.BusinessLogics.Interfaces;
using GameService.Infrastructure.Extensions;
using GameService.API.Gateways.Implementations;
using GameService.API.Gateways.Interfaces;

namespace GameService.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddBusinessLogics();
            services.AddGateways();
        }
        public static void AddBusinessLogics(this IServiceCollection services)
        {
            services.AddScoped<ICategoryBL, CategoryBL>();
            services.AddScoped<IGameBL, GameBL>();
            services.AddScoped<IAchievementBL, AchievementBL>();
            services.AddScoped<ISteamBL, SteamBL>();
            services.AddScoped<IPlaystationBL, PlaystationBL>();
            services.AddScoped<IPlatformBL, PlatformBL>();
            services.AddScoped<ISerieBL, SerieBL>();
        }

        public static void AddGateways(this IServiceCollection services)
        {
            services.AddHttpClient<ISteamApiGateway, SteamApiGateway>();
            services.AddHttpClient<IPlaystationApiGateway, PlaystationApiGateway>();
        }

    }
}
