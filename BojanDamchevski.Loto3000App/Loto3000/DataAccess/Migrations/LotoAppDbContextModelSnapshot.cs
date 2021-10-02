﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(LotoAppDbContext))]
    partial class LotoAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Domain.Models.Draw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Draws");
                });

            modelBuilder.Entity("Domain.Models.LotoNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LotoNumberChoice")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LotoNumbers");
                });

            modelBuilder.Entity("Domain.Models.Prize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PrizeNumber")
                        .HasColumnType("int");

                    b.Property<string>("PrizeType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prizes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PrizeNumber = 16,
                            PrizeType = "Car"
                        },
                        new
                        {
                            Id = 2,
                            PrizeNumber = 21,
                            PrizeType = "Vacation"
                        },
                        new
                        {
                            Id = 3,
                            PrizeNumber = 1,
                            PrizeType = "TV"
                        },
                        new
                        {
                            Id = 4,
                            PrizeNumber = 24,
                            PrizeType = "100$ Gift Card"
                        },
                        new
                        {
                            Id = 5,
                            PrizeNumber = 19,
                            PrizeType = "50$ GiftCard"
                        });
                });

            modelBuilder.Entity("Domain.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrawId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastDraw")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextDraw")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DrawId")
                        .IsUnique();

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.WinningNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrawId")
                        .HasColumnType("int");

                    b.Property<int>("WinningNum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrawId");

                    b.ToTable("WinningNumbers");
                });

            modelBuilder.Entity("Domain.Models.Draw", b =>
                {
                    b.HasOne("Domain.Models.Admin", "Admin")
                        .WithMany("Draws")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.LotoNumber", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("LotoNumbers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Session", b =>
                {
                    b.HasOne("Domain.Models.Draw", "Draw")
                        .WithOne("Session")
                        .HasForeignKey("Domain.Models.Session", "DrawId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.HasOne("Domain.Models.Admin", "Admin")
                        .WithMany("Users")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.WinningNumber", b =>
                {
                    b.HasOne("Domain.Models.Draw", "Draw")
                        .WithMany("WinningNumbers")
                        .HasForeignKey("DrawId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
