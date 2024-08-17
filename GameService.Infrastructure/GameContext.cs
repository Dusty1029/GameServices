using GameService.Infrastructure.Configurations;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure
{
    public class GameContext : DbContext
    {
        public DbSet<AchievementEntity> Games { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<AchievementEntity> Achievements { get; set; }

        public GameContext(DbContextOptions<GameContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfiguration).Assembly);
        }
    }
}