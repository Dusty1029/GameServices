using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class WishGameConfiguration : IEntityTypeConfiguration<WishGameEntity>
    {
        public void Configure(EntityTypeBuilder<WishGameEntity> builder)
        {
            builder.ToTable(nameof(WishGameEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

        }
    }
}
