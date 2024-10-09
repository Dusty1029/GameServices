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

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.ParameterEnum)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.Value)
                   .IsRequired();

            builder.HasData(ParametersSeed);

            //Indexes

            builder.HasIndex(x => x.ParameterEnum)
                   .IsUnique();
        }

        private static List<ParameterEntity> ParametersSeed => 
        [
            new()
            {
                Id = new Guid("0fb8c4e8-9180-4cc5-98ff-a905d43ac440"),
                ParameterEnum = ParameterEnumEntity.PlaystationToken
            },
            new()
            {
                Id = new Guid("17481d3a-84f4-4140-8d12-2aa09f1b8d02"),
                ParameterEnum = ParameterEnumEntity.Npsso
            }
        ];
    }
}
