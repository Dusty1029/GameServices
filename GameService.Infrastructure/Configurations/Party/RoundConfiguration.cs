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
    public class RoundConfiguration : IEntityTypeConfiguration<RoundEntity>
    {
        public void Configure(EntityTypeBuilder<RoundEntity> builder)
        {
            builder.ToTable(nameof(RoundEntity).ToTableName(), Schema.NAME);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.HasOne(x => x.Game)
                   .WithMany()
                   .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.TeamOne)
                   .WithMany()
                   .HasForeignKey(x => x.TeamOneId);

            builder.HasOne(x => x.TeamTwo)
                   .WithMany()
                   .HasForeignKey(x => x.TeamTwoId);

            builder.HasOne(x => x.Gage)
                   .WithMany()
                   .HasForeignKey(x => x.GageId);

            builder.HasOne(x => x.WinningTeam)
                   .WithMany()
                   .HasForeignKey(x => x.WinningTeamId);
        }
    }
}
