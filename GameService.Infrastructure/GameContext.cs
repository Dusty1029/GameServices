using GameService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfiguration).Assembly);
        }
    }
}