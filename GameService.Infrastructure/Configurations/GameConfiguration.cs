using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
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

            builder.Property(x => x.HowLongToBeatName)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.GlobalStatus)
                   .HasDefaultValue(GameDetailStatusEnumEntity.NotStarted)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.PlayOrder)
                   .HasDefaultValue(0)
                   .IsRequired();

            //Relations

            builder.HasOne(x => x.Serie)
                   .WithMany(x => x.Games)
                   .HasForeignKey(x => x.SerieId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Categories)
                   .WithMany(x => x.Games)
                   .UsingEntity($"{nameof(GameEntity).ToTableName()}{nameof(CategoryEntity).ToTableName()}");

            builder.HasMany(x => x.GameDetails)
                   .WithOne(x => x.Game)
                   .HasForeignKey(x => x.GameId);
        }
    }
}
