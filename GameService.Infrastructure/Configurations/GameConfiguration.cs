using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder.ToTable(nameof(GameEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.SteamId)
                   .IsRequired(false);

            builder.Property(x => x.PlaystationId)
                   .HasMaxLength(256)
                   .IsRequired(false);

            builder.Property(x => x.Platform)
                   .IsRequired();

            builder.Property(x => x.IsIgnored) 
                   .HasDefaultValue(false)
                   .IsRequired();

            //Relations
            builder.HasMany(x => x.Achievements)
                   .WithOne(x => x.Game)
                   .HasForeignKey(x => x.GameId);

            builder.HasMany(x => x.Categories)
                   .WithMany(x => x.Games)
                   .UsingEntity($"{nameof(GameEntity).ToTableName()}{nameof(CategoryEntity).ToTableName()}");

            //Indexes
            builder.HasIndex(x => x.SteamId)
                   .IsUnique();

            builder.HasIndex(x => new { x.PlaystationId, x.Platform })
                   .IsUnique();

            builder.HasIndex(x => new { x.Name, x.Platform})
                   .IsUnique();

        }
    }
}
