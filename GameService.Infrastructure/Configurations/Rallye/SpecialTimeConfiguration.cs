using CommonV2.Extensions;
using GameService.Infrastructure.Entities.Rallye;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GameService.Infrastructure.Configurations.Rallye
{
    public class SpecialTimeConfiguration : IEntityTypeConfiguration<SpecialTimeEntity>
    {
        public void Configure(EntityTypeBuilder<SpecialTimeEntity> builder)
        {
            builder.ToTable(nameof(SpecialTimeEntity).ToTableName(), Schema.NAME);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.HasOne(x => x.Player)
                   .WithMany(x => x.SpecialTimes)
                   .HasForeignKey(x => x.PlayerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}