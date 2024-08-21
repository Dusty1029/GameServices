using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class ParameterConfiguration : IEntityTypeConfiguration<ParameterEntity>
    {
        public void Configure(EntityTypeBuilder<ParameterEntity> builder)
        {
            builder.ToTable(nameof(ParameterEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ParameterEnum)
                   .IsRequired();

            builder.Property(x => x.Value)
                   .IsRequired();

            builder.HasData(new ParameterEntity 
            { 
                Id = new Guid("0fb8c4e8-9180-4cc5-98ff-a905d43ac440"),
                ParameterEnum = ParameterEnumEntity.PlaystationToken
            });

            //Indexes

            builder.HasIndex(x => x.ParameterEnum)
                   .IsUnique();
        }
    }
}
