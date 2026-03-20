using CommonV2.Extensions;
using GameService.Infrastructure.Entities.Rallye;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations.Rallye
{
    internal class RallyeConfiguration : IEntityTypeConfiguration<RallyeEntity>
    {
        public void Configure(EntityTypeBuilder<RallyeEntity> builder)
        {
            builder.ToTable(nameof(RallyeEntity).ToTableName(), Schema.NAME);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.HasMany(x => x.Specials)
                   .WithOne(s => s.Rallye)
                   .HasForeignKey(s => s.RallyeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Players).WithMany(p => p.Rallyes)
                   .UsingEntity($"{nameof(PlayerEntity).ToTableName()}{nameof(RallyeEntity).ToTableName()}");
        }
    }
}
