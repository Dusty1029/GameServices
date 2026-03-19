using GameService.Infrastructure.Repositories.Implementations.Party;
using GameService.Infrastructure.Repositories.Interfaces.Party;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Infrastructure.Extensions
{
    public static class IServiceCollectionPartyExtensions
    {
        public static void AddPartyRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGageRepository, GageRepository>();
            services.AddScoped<IPartyRepository, PartyRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
        }
    }
}
