using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class IgnoredGameConfiguration : IEntityTypeConfiguration<IgnoredGameEntity>
    {
        public void Configure(EntityTypeBuilder<IgnoredGameEntity> builder)
        {
            builder.ToTable(nameof(IgnoredGameEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.SteamId)
                   .IsRequired(false);

            builder.Property(x => x.PlaystationId)
                   .HasMaxLength(256)
                   .IsRequired(false);

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            //Relations

            builder.HasOne(x => x.Platform)
                   .WithMany(x => x.IgnoredGames)
                   .HasForeignKey(x => x.PlatformId);

            //Indexes

            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.SteamId).IsUnique();
            builder.HasIndex(x => x.PlaystationId).IsUnique();

        }
    }
}
