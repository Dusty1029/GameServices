﻿using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<AchievementEntity>
    {
        public void Configure(EntityTypeBuilder<AchievementEntity> builder)
        {
            builder.ToTable(nameof(AchievementEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.SteamName)
                   .HasMaxLength(256)
                   .IsRequired(false)
                   .HasDefaultValue(null);

            builder.Property(x => x.PlaystationTrophyId)
                   .IsRequired(false)
                   .HasDefaultValue(null);

            builder.Property(x => x.Description)
                   .HasMaxLength(512)
                   .IsRequired(false)
                   .HasDefaultValue(null);

            builder.Property(p => p.Percentage)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired(false)
                   .HasDefaultValue(null);

            builder.Property(p => p.Achieved)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(p => p.IsIgnored)
                   .IsRequired()
                   .HasDefaultValue(false);

            //Relations
            builder.HasOne(x => x.GameDetail)
                   .WithMany(x => x.Achievements)
                   .HasForeignKey(x => x.GameDetailId);


        }
    }
}
