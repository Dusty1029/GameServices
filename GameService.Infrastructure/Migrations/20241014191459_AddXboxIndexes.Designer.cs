﻿// <auto-generated />
using System;
using GameService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20241014191459_AddXboxIndexes")]
    partial class AddXboxIndexes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameCategory", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GamesId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoriesId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("GameCategory");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.AchievementEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<bool>("Achieved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<Guid>("GameDetailId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsIgnored")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int?>("PlaystationTrophyId")
                        .HasColumnType("integer");

                    b.Property<string>("SteamName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int?>("XboxId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameDetailId");

                    b.ToTable("Achievement", (string)null);
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<bool>("IsSeed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7958d93d-852e-4c14-8b55-9d6620821126"),
                            IsSeed = true,
                            Name = "Action - aventure"
                        },
                        new
                        {
                            Id = new Guid("952981cc-bfad-4115-aa41-c2f2d4b31446"),
                            IsSeed = true,
                            Name = "FPS"
                        },
                        new
                        {
                            Id = new Guid("87ad5c12-c33b-45a4-b0f3-d95f0758e765"),
                            IsSeed = true,
                            Name = "RPG"
                        },
                        new
                        {
                            Id = new Guid("a376bb7c-0d37-44db-981a-9a44c8583e05"),
                            IsSeed = true,
                            Name = "TPS"
                        },
                        new
                        {
                            Id = new Guid("903b63a1-19bc-4970-af43-ae99a3b4eb92"),
                            IsSeed = true,
                            Name = "Plateforme"
                        },
                        new
                        {
                            Id = new Guid("e93c812a-17e4-4849-b6ce-a98353a787e3"),
                            IsSeed = true,
                            Name = "Jeux de sport et de course"
                        },
                        new
                        {
                            Id = new Guid("649c04ce-1936-4f55-9d2b-e8448e9a05df"),
                            IsSeed = true,
                            Name = "Jeux de combat"
                        });
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameDetailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uuid");

                    b.Property<string>("PlaystationId")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("NotStarted");

                    b.Property<int?>("SteamId")
                        .HasColumnType("integer");

                    b.Property<string>("XboxId")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.HasIndex("SteamId")
                        .IsUnique();

                    b.HasIndex("GameId", "PlatformId")
                        .IsUnique();

                    b.HasIndex("PlaystationId", "PlatformId")
                        .IsUnique();

                    b.HasIndex("XboxId", "PlatformId")
                        .IsUnique();

                    b.ToTable("GameDetail", (string)null);
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("PlayOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<Guid?>("SerieId")
                        .HasColumnType("uuid");

                    b.Property<int>("StatusOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(3);

                    b.HasKey("Id");

                    b.HasIndex("SerieId");

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GoalEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<Guid>("GameDetailId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsFulFilled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("GameDetailId");

                    b.ToTable("Goal", (string)null);
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.IgnoredGameEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uuid");

                    b.Property<string>("PlaystationId")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int?>("SteamId")
                        .HasColumnType("integer");

                    b.Property<string>("XboxId")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("PlatformId");

                    b.HasIndex("SteamId")
                        .IsUnique();

                    b.HasIndex("PlaystationId", "PlatformId")
                        .IsUnique();

                    b.HasIndex("XboxId", "PlatformId")
                        .IsUnique();

                    b.ToTable("IgnoredGame", (string)null);
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.ParameterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("ParameterEnum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParameterEnum")
                        .IsUnique();

                    b.ToTable("Parameter", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0fb8c4e8-9180-4cc5-98ff-a905d43ac440"),
                            ParameterEnum = "PlaystationToken",
                            Value = ""
                        },
                        new
                        {
                            Id = new Guid("17481d3a-84f4-4140-8d12-2aa09f1b8d02"),
                            ParameterEnum = "Npsso",
                            Value = ""
                        });
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.PlatformEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<bool>("IsSeed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PlatformEnum")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PlatformEnum")
                        .IsUnique();

                    b.ToTable("Platform", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("450ba6e2-97a6-4dcd-87d5-607f60385821"),
                            IsSeed = true,
                            Name = "Steam",
                            PlatformEnum = "Steam"
                        },
                        new
                        {
                            Id = new Guid("9b77ae73-ee99-46f3-a84d-f337ced142a8"),
                            IsSeed = true,
                            Name = "PS VITA",
                            PlatformEnum = "PSVITA"
                        },
                        new
                        {
                            Id = new Guid("793511b2-5d1c-4b47-b6a7-4ced103b0be3"),
                            IsSeed = true,
                            Name = "PS3",
                            PlatformEnum = "PS3"
                        },
                        new
                        {
                            Id = new Guid("43d4fe37-f799-4d61-befb-404d76f7a759"),
                            IsSeed = true,
                            Name = "PS4",
                            PlatformEnum = "PS4"
                        },
                        new
                        {
                            Id = new Guid("db5275b2-1507-4b47-b721-b516b6b5c0d0"),
                            IsSeed = true,
                            Name = "PS5",
                            PlatformEnum = "PS5"
                        },
                        new
                        {
                            Id = new Guid("b7943964-819d-4c70-9997-c09c7aeb468d"),
                            IsSeed = true,
                            Name = "Xbox360",
                            PlatformEnum = "Xbox360"
                        });
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.SerieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<bool>("IsDefault")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("ParentSerieId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentSerieId");

                    b.ToTable("Serie", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("93d2da48-aaea-4932-afec-8a2ae310edd3"),
                            IsDefault = true,
                            Name = "Pas de série"
                        });
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.WishGameEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("WishGame", (string)null);
                });

            modelBuilder.Entity("GameCategory", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.CategoryEntity", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameService.Infrastructure.Entities.GameEntity", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.AchievementEntity", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.GameDetailEntity", "GameDetail")
                        .WithMany("Achievements")
                        .HasForeignKey("GameDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameDetail");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameDetailEntity", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.GameEntity", "Game")
                        .WithMany("GameDetails")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameService.Infrastructure.Entities.PlatformEntity", "Platform")
                        .WithMany("GameDetails")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameEntity", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.SerieEntity", "Serie")
                        .WithMany("Games")
                        .HasForeignKey("SerieId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Serie");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GoalEntity", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.GameDetailEntity", "GameDetail")
                        .WithMany("Goals")
                        .HasForeignKey("GameDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameDetail");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.IgnoredGameEntity", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.PlatformEntity", "Platform")
                        .WithMany("IgnoredGames")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.SerieEntity", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.SerieEntity", "ParentSerie")
                        .WithMany("ChildrenSeries")
                        .HasForeignKey("ParentSerieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ParentSerie");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.WishGameEntity", b =>
                {
                    b.HasOne("GameService.Infrastructure.Entities.PlatformEntity", "Platform")
                        .WithMany("WishGames")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameDetailEntity", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("Goals");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameEntity", b =>
                {
                    b.Navigation("GameDetails");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.PlatformEntity", b =>
                {
                    b.Navigation("GameDetails");

                    b.Navigation("IgnoredGames");

                    b.Navigation("WishGames");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.SerieEntity", b =>
                {
                    b.Navigation("ChildrenSeries");

                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
