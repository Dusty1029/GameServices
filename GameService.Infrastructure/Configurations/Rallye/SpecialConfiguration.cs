using CommonV2.Extensions;
using GameService.Infrastructure.Entities.Rallye;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations.Rallye
{
    public class SpecialConfiguration : IEntityTypeConfiguration<SpecialEntity>
    {
        public void Configure(EntityTypeBuilder<SpecialEntity> builder)
        {
            builder.ToTable(nameof(SpecialEntity).ToTableName(), Schema.NAME);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.HasMany(x => x.SpecialTimes)
                   .WithOne(s => s.Special)
                   .HasForeignKey(s => s.SpecialId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
