using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class SerieConfiguration : IEntityTypeConfiguration<SerieEntity>
    {
        public void Configure(EntityTypeBuilder<SerieEntity> builder)
        {
            builder.ToTable(nameof(SerieEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.IsDefault)
                   .IsRequired()
                   .HasDefaultValue(false);

            //Relations
            builder.HasOne(x => x.ParentSerie)
                   .WithMany(x => x.ChildrenSeries)
                   .HasForeignKey(x => x.ParentSerieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Name)
                   .IsUnique();

            builder.HasData(
            [
                new() 
                {
                    Id = new Guid("93d2da48-aaea-4932-afec-8a2ae310edd3"),
                    Name = "Pas de série",
                    IsDefault = true
                }
            ]);
        }
    }
}
