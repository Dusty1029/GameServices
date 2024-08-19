﻿// <auto-generated />
using System;
using GameService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20240819221612_AddPlaystationIdToGame")]
    partial class AddPlaystationIdToGame
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameCategory", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriesId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("GameCategory");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.AchievementEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Achieved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("SteamName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Achievement", (string)null);
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsSeed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("2346a66c-0f40-4ea6-b61d-582865896b41"),
                            IsSeed = true,
                            Name = "Jeux commencés"
                        },
                        new
                        {
                            Id = new Guid("78b7740c-0a69-4dab-a4b1-14a39457a81a"),
                            IsSeed = true,
                            Name = "Jeux terminés"
                        },
                        new
                        {
                            Id = new Guid("2c9a7b53-183f-4971-9b8a-81c98816f05f"),
                            IsSeed = true,
                            Name = "Jeux terminés à 100%"
                        },
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

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsIgnored")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Platform")
                        .HasColumnType("int");

                    b.Property<string>("PlaystationId")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("SteamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlaystationId")
                        .IsUnique()
                        .HasFilter("[PlaystationId] IS NOT NULL");

                    b.HasIndex("SteamId")
                        .IsUnique()
                        .HasFilter("[SteamId] IS NOT NULL");

                    b.HasIndex("Name", "Platform")
                        .IsUnique();

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.ParameterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ParameterEnum")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParameterEnum")
                        .IsUnique();

                    b.ToTable("Parameter", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0fb8c4e8-9180-4cc5-98ff-a905d43ac440"),
                            ParameterEnum = 0,
                            Value = ""
                        });
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
                    b.HasOne("GameService.Infrastructure.Entities.GameEntity", "Game")
                        .WithMany("Achievements")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("GameService.Infrastructure.Entities.GameEntity", b =>
                {
                    b.Navigation("Achievements");
                });
#pragma warning restore 612, 618
        }
    }
}
