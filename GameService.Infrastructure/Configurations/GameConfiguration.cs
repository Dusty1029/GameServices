using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
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
            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            //Relations

            builder.HasOne(x => x.Serie)
                   .WithMany(x => x.Games)
                   .HasForeignKey(x => x.SerieId);

            builder.HasMany(x => x.Categories)
                   .WithMany(x => x.Games)
                   .UsingEntity($"{nameof(GameEntity).ToTableName()}{nameof(CategoryEntity).ToTableName()}");

            builder.HasMany(x => x.GameDetails)
                   .WithOne(x => x.Game)
                   .HasForeignKey(x => x.GameId);
        }
    }
}
