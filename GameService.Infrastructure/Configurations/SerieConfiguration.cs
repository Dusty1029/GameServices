﻿using CommonV2.Extensions;
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

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            //Relations
            builder.HasOne(x => x.ParentSerie)
                   .WithMany(x => x.ChildrenSeries)
                   .HasForeignKey(x => x.ParentSerieId);
        }
    }
}