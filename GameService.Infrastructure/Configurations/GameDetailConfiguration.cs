﻿using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class GameDetailConfiguration : IEntityTypeConfiguration<GameDetailEntity>
    {
        public void Configure(EntityTypeBuilder<GameDetailEntity> builder)
        {
            builder.ToTable(nameof(GameDetailEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SteamId)
                   .IsRequired(false);

            builder.Property(x => x.PlaystationId)
                   .HasMaxLength(256)
                   .IsRequired(false);

            builder.Property(x => x.IsFinished)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.IsStarted)
                   .HasDefaultValue(false)
                   .IsRequired();

            //Relations
            builder.HasMany(x => x.Achievements)
                   .WithOne(x => x.GameDetail)
                   .HasForeignKey(x => x.GameDetailId);

            builder.HasOne(x => x.Platform)
                   .WithMany(x => x.GameDetails)
                   .HasForeignKey(x => x.PlatformId);

            builder.HasMany(x => x.Goals)
                   .WithOne(x => x.GameDetail)
                   .HasForeignKey(x => x.GameDetailId);

            //Indexes
            builder.HasIndex(x => x.SteamId)
                   .IsUnique();

            builder.HasIndex(x => new { x.PlaystationId, x.PlatformId })
                   .IsUnique();

            builder.HasIndex(x => new { x.GameId, x.PlatformId })
                   .IsUnique();
        }
    }
}