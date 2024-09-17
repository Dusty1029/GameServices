using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class GoalConfiguration : IEntityTypeConfiguration<GoalEntity>
    {
        public void Configure(EntityTypeBuilder<GoalEntity> builder)
        {
            builder.ToTable(nameof(GoalEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(512)
                   .IsRequired();

            builder.Property(x => x.IsFulFilled)
                   .HasDefaultValue(false)
                   .IsRequired();
        }
    }
}
