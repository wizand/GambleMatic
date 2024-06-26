﻿// <auto-generated />
using System;
using GambleMaticDataLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    [DbContext(typeof(GambleMaticContext))]
    [Migration("20221115173756_Goals_is_a_string")]
    partial class Goals_is_a_string
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GambleItemModel", b =>
                {
                    b.Property<long>("GambleItemModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GambleItemModelId"), 1L, 1);

                    b.Property<int?>("GameModelId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Guess")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerModelId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("GambleItemModelId");

                    b.HasIndex("GameModelId");

                    b.HasIndex("PlayerModelId");

                    b.ToTable("Gambles");
                });

            modelBuilder.Entity("GambleMaticDataLib.ExtraGamblesModel", b =>
                {
                    b.Property<long>("ExtraGamblesModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ExtraGamblesModelId"), 1L, 1);

                    b.Property<string>("GoalsInTournamen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoldTeam")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PlayerModelId")
                        .HasColumnType("int");

                    b.Property<string>("SemifinalTeamFour")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SemifinalTeamOne")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SemifinalTeamThree")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SemifinalTeamTwo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SilverTeam")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ExtraGamblesModelId");

                    b.HasIndex("PlayerModelId")
                        .IsUnique();

                    b.ToTable("ExtraGambles");
                });

            modelBuilder.Entity("GameModel", b =>
                {
                    b.Property<int>("GameModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameModelId"), 1L, 1);

                    b.Property<string>("Away")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("GameDay")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameOrderSortId")
                        .HasColumnType("int");

                    b.Property<string>("Home")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ResultInt")
                        .HasColumnType("int");

                    b.HasKey("GameModelId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PlayerModel", b =>
                {
                    b.Property<int>("PlayerModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerModelId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Paid")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Ticket")
                        .HasColumnType("int");

                    b.HasKey("PlayerModelId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("GambleItemModel", b =>
                {
                    b.HasOne("GameModel", "GameModel")
                        .WithMany()
                        .HasForeignKey("GameModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlayerModel", "PlayerModel")
                        .WithMany("GambleItemModels")
                        .HasForeignKey("PlayerModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameModel");

                    b.Navigation("PlayerModel");
                });

            modelBuilder.Entity("GambleMaticDataLib.ExtraGamblesModel", b =>
                {
                    b.HasOne("PlayerModel", "PlayerModel")
                        .WithOne("ExtraGambles")
                        .HasForeignKey("GambleMaticDataLib.ExtraGamblesModel", "PlayerModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerModel");
                });

            modelBuilder.Entity("PlayerModel", b =>
                {
                    b.Navigation("ExtraGambles")
                        .IsRequired();

                    b.Navigation("GambleItemModels");
                });
#pragma warning restore 612, 618
        }
    }
}
