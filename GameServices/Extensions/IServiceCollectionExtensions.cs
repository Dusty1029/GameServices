using GameService.Infrastructure.Extensions;
using GameServices.API.BusinessLogics.Implementations;
using GameServices.API.BusinessLogics.Interfaces;
using GameServices.API.Gateways.Implementations;
using GameServices.API.Gateways.Interfaces;

namespace GameServices.API.Extensions
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
        }

        public static void AddGateways(this IServiceCollection services)
        {
            services.AddScoped<ISteamApiGateway, SteamApiGateway>();
        }
    }
}
