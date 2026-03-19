using CommonV2.Extensions;
using GameService.Infrastructure.Entities.Party;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations.Party
{
    public class TeamConfiguration : IEntityTypeConfiguration<TeamEntity>
    {
        public void Configure(EntityTypeBuilder<TeamEntity> builder)
        {
            builder.ToTable(nameof(TeamEntity).ToTableName(), Schema.NAME);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.HasMany(x => x.Players).WithMany(p => p.Teams)
                   .UsingEntity($"{nameof(TeamEntity).ToTableName()}{nameof(PlayerEntity).ToTableName()}"); ;
        }
    }
}
