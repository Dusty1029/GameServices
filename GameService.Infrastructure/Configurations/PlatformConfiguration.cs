using CommonV2.Extensions;
using GameService.Infrastructure.Entities;
using GameService.Infrastructure.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameService.Infrastructure.Configurations
{
    public class PlatformConfiguration : IEntityTypeConfiguration<PlatformEntity>
    {
        public void Configure(EntityTypeBuilder<PlatformEntity> builder)
        {
            builder.ToTable(nameof(PlatformEntity).ToTableName());

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.IsSeed)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.PlatformEnum)
                   .HasDefaultValue(null)
                   .HasConversion<string>()
                   .IsRequired(false);

            //Relations
            builder.HasMany(x => x.WishGames)
                   .WithOne(x => x.Platform)
                   .HasForeignKey(x => x.PlatformId);

            //Indexes

            builder.HasIndex(x => x.PlatformEnum)
                   .IsUnique();

            builder.HasIndex(x => x.Name)
                   .IsUnique();

            //Seed
            builder.HasData(PlatformsSeed);
        }

        public static List<PlatformEntity> PlatformsSeed =>
        [
            new()
            {
                Id = new Guid("450ba6e2-97a6-4dcd-87d5-607f60385821"),
                IsSeed = true,
                Name = "Steam",
                PlatformEnum = PlatformEnumEntity.Steam
            },
            new()
            {
                Id = new Guid("9b77ae73-ee99-46f3-a84d-f337ced142a8"),
                IsSeed = true,
                Name = "PS VITA",
                PlatformEnum = PlatformEnumEntity.PSVITA
            },
            new()
            {
                Id = new Guid("793511b2-5d1c-4b47-b6a7-4ced103b0be3"),
                IsSeed = true,
                Name = "PS3",
                PlatformEnum = PlatformEnumEntity.PS3
            },
            new()
            {
                Id = new Guid("43d4fe37-f799-4d61-befb-404d76f7a759"),
                IsSeed = true,
                Name = "PS4",
                PlatformEnum = PlatformEnumEntity.PS4
            },
            new()
            {
                Id = new Guid("db5275b2-1507-4b47-b721-b516b6b5c0d0"),
                IsSeed = true,
                Name = "PS5",
                PlatformEnum = PlatformEnumEntity.PS5
            },
            new()
            {
                Id = new Guid("b7943964-819d-4c70-9997-c09c7aeb468d"),
                IsSeed = true,
                Name = "Xbox360",
                PlatformEnum = PlatformEnumEntity.Xbox360
            }
        ];
    }
}
