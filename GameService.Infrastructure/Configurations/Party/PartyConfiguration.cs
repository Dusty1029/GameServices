using CommonV2.Extensions;
using GameService.Infrastructure.Entities.Party;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Configurations.Party
{
    public class PartyConfiguration : IEntityTypeConfiguration<PartyEntity>
    {
        public void Configure(EntityTypeBuilder<PartyEntity> builder)
        {
            builder.ToTable(nameof(PartyEntity).ToTableName(), Schema.NAME);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.IsFinish)
                   .HasDefaultValue(false);

            builder.HasMany(x => x.Teams)
                   .WithOne(p => p.Party)
                   .HasForeignKey(p => p.PartyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Rounds)
                   .WithOne(r => r.Party)
                   .HasForeignKey(r => r.PartyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
