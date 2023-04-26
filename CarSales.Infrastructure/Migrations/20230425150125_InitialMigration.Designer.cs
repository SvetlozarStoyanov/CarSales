﻿// <auto-generated />
using System;
using CarSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarSales.Web.Data.Migrations
{
    [DbContext(typeof(CarSalesDbContext))]
    [Migration("20230425150125_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = "b5fef437-f504-46d2-926d-3158e54e1932"
                        });
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SalesmanId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SalesmanId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Sales");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OwnerId = 1,
                            SalePrice = 20000m,
                            SalesmanId = 1,
                            VehicleId = 3
                        });
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Salesman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SalesmanRating")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Salesmen");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SalesmanRating = 3,
                            UserId = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"
                        });
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Credits")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c3fbde61-1c9a-426b-ad87-2ed58b859b78",
                            Credits = 0m,
                            Email = "Admin@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            LastName = "Test",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEIl6zinUsWsv05Iba3zLGijrxVFeXvRzG39Kp3izwDI48lM6/Pyz7bDZUsapeJB52g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e08a71cd-4c61-4705-a67d-51679c84fd2f",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = "b5fef437-f504-46d2-926d-3158e54e1932",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "50236cc1-5f3a-4330-b5c3-e6cbfd48bae4",
                            Credits = 0m,
                            Email = "Owner@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Owner",
                            LastName = "Test",
                            LockoutEnabled = false,
                            NormalizedEmail = "OWNER@GMAIL.COM",
                            NormalizedUserName = "OWNER",
                            PasswordHash = "AQAAAAIAAYagAAAAEFjGJZN8Y5m3Sx0uIHxlmjPyeDq1yXQPAnLj3GyHw0hgWzVkET5vyzHx15TZTmfCXw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5d2c6176-e88d-444d-a2e5-5c80faa12b40",
                            TwoFactorEnabled = false,
                            UserName = "Owner"
                        },
                        new
                        {
                            Id = "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "07581444-301f-4b13-b205-e005fba7803c",
                            Credits = 0m,
                            Email = "Owner2@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Owner2",
                            LastName = "Test",
                            LockoutEnabled = false,
                            NormalizedEmail = "OWNER2@GMAIL.COM",
                            NormalizedUserName = "OWNER2",
                            PasswordHash = "AQAAAAIAAYagAAAAEJF8w0wYC9LlCAad8pD8zMqoQdgFIjoQ/m1moY4yJK4OTSIf3MFY0tojpt5ux9PvMQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c51bdf0a-25e0-44c6-9f45-3d2a960bb9ce",
                            TwoFactorEnabled = false,
                            UserName = "Owner2"
                        },
                        new
                        {
                            Id = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3bc7cac9-a1e9-49e7-9d53-821277999f75",
                            Credits = 0m,
                            Email = "Salesman@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Salesman",
                            LastName = "Test",
                            LockoutEnabled = false,
                            NormalizedEmail = "SALESMAN@GMAIL.COM",
                            NormalizedUserName = "SALESMAN",
                            PasswordHash = "AQAAAAIAAYagAAAAEJ5ZzzwKK086LMMBQ8aQ+rq+cbosdiwhmk285WrMCDpRXs5ea4UZbtQcvafds1DiLA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "543be2a1-d0dc-46e0-a2f9-665af505bbf3",
                            TwoFactorEnabled = false,
                            UserName = "Salesman"
                        });
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("KilometersDriven")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SalesmanId")
                        .HasColumnType("int");

                    b.Property<double>("TopSpeed")
                        .HasColumnType("float");

                    b.Property<int>("VehicleRating")
                        .HasColumnType("int");

                    b.Property<int>("VehicleType")
                        .HasColumnType("int");

                    b.Property<int>("YearProduced")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SalesmanId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "BMW",
                            Description = "Fast car",
                            KilometersDriven = 0.0,
                            Model = "M5",
                            Price = 9000m,
                            SalesmanId = 1,
                            TopSpeed = 250.0,
                            VehicleRating = 3,
                            VehicleType = 1,
                            YearProduced = 2016
                        },
                        new
                        {
                            Id = 2,
                            Brand = "BMW",
                            Description = "Classic car",
                            KilometersDriven = 0.0,
                            Model = "M3",
                            Price = 5000m,
                            SalesmanId = 1,
                            TopSpeed = 240.0,
                            VehicleRating = 3,
                            VehicleType = 1,
                            YearProduced = 2004
                        },
                        new
                        {
                            Id = 3,
                            Brand = "BMW",
                            Description = "Popular car",
                            KilometersDriven = 2000.0,
                            Model = "X5",
                            OwnerId = 1,
                            Price = 18000m,
                            TopSpeed = 243.0,
                            VehicleRating = 4,
                            VehicleType = 1,
                            YearProduced = 2020
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                            RoleId = "dacb7d40-e742-435c-b131-145300f3c97d"
                        },
                        new
                        {
                            UserId = "b5fef437-f504-46d2-926d-3158e54e1932",
                            RoleId = "bbea2448-c801-43d1-8b05-e3a2c22338d9"
                        },
                        new
                        {
                            UserId = "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                            RoleId = "bbea2448-c801-43d1-8b05-e3a2c22338d9"
                        },
                        new
                        {
                            UserId = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                            RoleId = "c63016c0-e087-43dc-bb9c-a8958a05cbdd"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Role", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasDiscriminator().HasValue("Role");

                    b.HasData(
                        new
                        {
                            Id = "dacb7d40-e742-435c-b131-145300f3c97d",
                            ConcurrencyStamp = "dacb7d40-e742-435c-b131-145300f3c97d",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR",
                            Description = "Admin"
                        },
                        new
                        {
                            Id = "bbea2448-c801-43d1-8b05-e3a2c22338d9",
                            ConcurrencyStamp = "bbea2448-c801-43d1-8b05-e3a2c22338d9",
                            Name = "Owner",
                            NormalizedName = "OWNER",
                            Description = "Can buy cars."
                        },
                        new
                        {
                            Id = "c63016c0-e087-43dc-bb9c-a8958a05cbdd",
                            ConcurrencyStamp = "c63016c0-e087-43dc-bb9c-a8958a05cbdd",
                            Name = "Salesman",
                            NormalizedName = "SALESMAN",
                            Description = "Can buy and sell cars."
                        });
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Owner", b =>
                {
                    b.HasOne("CarSales.Infrastructure.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Sale", b =>
                {
                    b.HasOne("CarSales.Infrastructure.Data.Entities.Owner", "Owner")
                        .WithMany("Sales")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarSales.Infrastructure.Data.Entities.Salesman", "Salesman")
                        .WithMany("Sales")
                        .HasForeignKey("SalesmanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarSales.Infrastructure.Data.Entities.Vehicle", "Vehicle")
                        .WithMany("Sales")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Salesman");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Salesman", b =>
                {
                    b.HasOne("CarSales.Infrastructure.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Vehicle", b =>
                {
                    b.HasOne("CarSales.Infrastructure.Data.Entities.Owner", "Owner")
                        .WithMany("Vehicles")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CarSales.Infrastructure.Data.Entities.Salesman", "Salesman")
                        .WithMany("Vehicles")
                        .HasForeignKey("SalesmanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Owner");

                    b.Navigation("Salesman");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CarSales.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarSales.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarSales.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarSales.Infrastructure.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Owner", b =>
                {
                    b.Navigation("Sales");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Salesman", b =>
                {
                    b.Navigation("Sales");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("CarSales.Infrastructure.Data.Entities.Vehicle", b =>
                {
                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}