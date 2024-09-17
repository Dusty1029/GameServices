using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class IgnoredSteamGameConfiguration : IEntityTypeConfiguration<IgnoredSteamGameEntity>
    {
        public void Configure(EntityTypeBuilder<IgnoredSteamGameEntity> builder)
        {
            builder.ToTable(nameof(IgnoredSteamGameEntity).ToTableName());

            builder.HasKey(x => x.SteamId);

            builder.Property(x => x.SteamId)
                   .ValueGeneratedNever();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();
        }
    }
}
