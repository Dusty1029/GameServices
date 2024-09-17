using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable(nameof(CategoryEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.IsSeed)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasData(CategoriesSeeded);
        }

        private static List<CategoryEntity> CategoriesSeeded => new() 
        {
            new()
            {
                Id = new Guid("7958d93d-852e-4c14-8b55-9d6620821126"),
                Name = "Action - aventure",
                IsSeed = true
            },
            new()
            {
                Id = new Guid("952981cc-bfad-4115-aa41-c2f2d4b31446"),
                Name = "FPS",
                IsSeed = true
            },
            new()
            {
                Id = new Guid("87ad5c12-c33b-45a4-b0f3-d95f0758e765"),
                Name = "RPG",
                IsSeed = true
            },
            new()
            {
                Id = new Guid("a376bb7c-0d37-44db-981a-9a44c8583e05"),
                Name = "TPS",
                IsSeed = true
            },
            new()
            {
                Id = new Guid("903b63a1-19bc-4970-af43-ae99a3b4eb92"),
                Name = "Plateforme",
                IsSeed = true
            },
            new()
            {
                Id = new Guid("e93c812a-17e4-4849-b6ce-a98353a787e3"),
                Name = "Jeux de sport et de course",
                IsSeed = true
            }
            ,
            new()
            {
                Id = new Guid("649c04ce-1936-4f55-9d2b-e8448e9a05df"),
                Name = "Jeux de combat",
                IsSeed = true
            }
        };
    }
}
