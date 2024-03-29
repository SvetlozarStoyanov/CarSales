﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarSales.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Credits",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Importers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImporterRating = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Importers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Importers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ShortReviewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StandartReviewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PremiumReviewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviewers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviewers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleRequests_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Salesmen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SalesmanRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesmen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salesmen_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearProduced = table.Column<int>(type: "int", nullable: false),
                    TopSpeed = table.Column<double>(type: "float", nullable: false),
                    KilometersDriven = table.Column<double>(type: "float", nullable: false),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    SalesmanId = table.Column<int>(type: "int", nullable: true),
                    ImporterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Importers_ImporterId",
                        column: x => x.ImporterId,
                        principalTable: "Importers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "Salesmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    OfferorId = table.Column<int>(type: "int", nullable: false),
                    SalesmanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Owners_OfferorId",
                        column: x => x.OfferorId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "Salesmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Performance = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Interior = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Longevity = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Features = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewType = table.Column<int>(type: "int", nullable: false),
                    ReviewStatus = table.Column<int>(type: "int", nullable: false),
                    VehicleRating = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehiclePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesmanId = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    ImporterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Importers_ImporterId",
                        column: x => x.ImporterId,
                        principalTable: "Importers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Salesmen_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "Salesmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d1b88f7-208a-43de-bb14-aca56b43080c", "2d1b88f7-208a-43de-bb14-aca56b43080c", "Can buy and review vehicles.", "Role", "Reviewer", "REVIEWER" },
                    { "9cbd5531-0c49-4889-95b9-b81fc1e7653a", "9cbd5531-0c49-4889-95b9-b81fc1e7653a", "Can buy and import vehicles.", "Role", "Importer", "IMPORTER" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "bbea2448-c801-43d1-8b05-e3a2c22338d9", "Can buy vehicles.", "Role", "Owner", "OWNER" },
                    { "c63016c0-e087-43dc-bb9c-a8958a05cbdd", "c63016c0-e087-43dc-bb9c-a8958a05cbdd", "Can buy and sell vehicles.", "Role", "Salesman", "SALESMAN" },
                    { "dacb7d40-e742-435c-b131-145300f3c97d", "dacb7d40-e742-435c-b131-145300f3c97d", "Admin", "Role", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Credits", "Email", "EmailConfirmed", "FirstName", "Gender", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "10933c11-ac2a-410d-b60a-8b1d97324975", 0, "d25d7163-542d-43a3-864d-a1ac3762b0a8", 50000m, "importer@gmail.com", false, "Importer", 1, null, "Test", false, null, "IMPORTER@GMAIL.COM", "IMPORTER", "AQAAAAIAAYagAAAAELv8x3uGx7uD0LUb5fNDBtw6DVSEIX/nrIdP+803c26F5K1GBYDIAizRJN80IZlABA==", null, false, "c322ead5-80ae-4f03-9fe4-85fa5e5ba602", false, "Importer" },
                    { "4d693871-c20b-4e9f-8490-1c641b9e3a40", 0, "55457c68-2018-4026-be62-9806627ee236", 50000m, "reviewer@gmail.com", false, "Reviewer", 1, null, "Test", false, null, "REVIEWER@GMAIL.COM", "REVIEWER", "AQAAAAIAAYagAAAAEFzXWB+RxXmIfrkUzlS8CbchQGtI/rCDi2yURLSVdXI0icWkX/BWtiHchXd7VKCkFw==", null, false, "26885ff7-a0d4-4347-951b-c8fbe5fd8e50", false, "Reviewer" },
                    { "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", 0, "720b7875-8565-4173-b419-7d33708939c2", 50000m, "salesman@gmail.com", false, "Salesman", 1, null, "Test", false, null, "SALESMAN@GMAIL.COM", "SALESMAN", "AQAAAAIAAYagAAAAELs4rn66IOlbs2ewhOD4G/PQ/0h7jDpe3nn3Gvdy4KHel4X6H6urava2UrKoqhKopg==", null, false, "11a1bc09-ed1f-4055-907e-3b5e55f6b344", false, "Salesman" },
                    { "926bee86-8bbd-43f6-bc1c-9639d43531a4", 0, "4409d31c-5724-40ad-861e-fe7ebfcd76f3", 50000m, "owner2@gmail.com", false, "Owner2", 1, null, "Test", false, null, "OWNER2@GMAIL.COM", "OWNER2", "AQAAAAIAAYagAAAAEJGWgqU6qUL+WqOHoyGd0e/u6JrWVhSyA4tDoOE89UKISRanDaffZEYtmbYs9p2zNg==", null, false, "b52c268f-424e-49cb-a5b1-c87a8b72a5b5", false, "Owner2" },
                    { "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d", 0, "8ffb9c20-68ef-4f7b-b125-6719cd6524e7", 50000m, "reviewer2@gmail.com", false, "Reviewer2", 1, null, "Test", false, null, "REVIEWER2@GMAIL.COM", "REVIEWER2", "AQAAAAIAAYagAAAAEM08/AJ1RZmHLvyYGxq3E9rWFY37UOQFcqBcSfvjHHSLbMXC7L/a5ZF7oyPZr4r7uQ==", null, false, "3a4052bb-b6b8-428d-a39b-8f93c55a4c7d", false, "Reviewer2" },
                    { "b5fef437-f504-46d2-926d-3158e54e1932", 0, "03223bf1-5f65-4d91-8059-0bd8095917b3", 50000m, "owner@gmail.com", false, "Owner", 1, null, "Test", false, null, "OWNER@GMAIL.COM", "OWNER", "AQAAAAIAAYagAAAAEHimnkQ3/OcB/tzPBP8guGlK7/ZzbQQNedgZ2TrKXDaixDvZThFP5bWvZwH+RxTBKw==", null, false, "a2b6c729-8b13-4098-99b3-b93751b9aba1", false, "Owner" },
                    { "cbed6d2a-e60a-49df-a6e3-982ccd980393", 0, "9c7c0235-d9b3-4b94-ba0b-86bed94177be", 50000m, "admin@gmail.com", false, "Admin", 1, null, "Test", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAU1L51EOP8VQvW0HWM6Xf2T+H+9YxDySJeqv3CQFXyILMP7BNaJ7nIXklY46GRpxQ==", null, false, "143d6784-2132-4919-9b2d-4b41bfd1c0ae", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9cbd5531-0c49-4889-95b9-b81fc1e7653a", "10933c11-ac2a-410d-b60a-8b1d97324975" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "10933c11-ac2a-410d-b60a-8b1d97324975" },
                    { "2d1b88f7-208a-43de-bb14-aca56b43080c", "4d693871-c20b-4e9f-8490-1c641b9e3a40" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "4d693871-c20b-4e9f-8490-1c641b9e3a40" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" },
                    { "c63016c0-e087-43dc-bb9c-a8958a05cbdd", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "926bee86-8bbd-43f6-bc1c-9639d43531a4" },
                    { "2d1b88f7-208a-43de-bb14-aca56b43080c", "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "b5fef437-f504-46d2-926d-3158e54e1932" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "cbed6d2a-e60a-49df-a6e3-982ccd980393" },
                    { "dacb7d40-e742-435c-b131-145300f3c97d", "cbed6d2a-e60a-49df-a6e3-982ccd980393" }
                });

            migrationBuilder.InsertData(
                table: "Importers",
                columns: new[] { "Id", "ImporterRating", "IsActive", "UserId" },
                values: new object[] { 1, 3, true, "10933c11-ac2a-410d-b60a-8b1d97324975" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, "b5fef437-f504-46d2-926d-3158e54e1932" },
                    { 2, "926bee86-8bbd-43f6-bc1c-9639d43531a4" },
                    { 3, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" },
                    { 4, "10933c11-ac2a-410d-b60a-8b1d97324975" },
                    { 5, "4d693871-c20b-4e9f-8490-1c641b9e3a40" },
                    { 6, "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d" },
                    { 7, "cbed6d2a-e60a-49df-a6e3-982ccd980393" }
                });

            migrationBuilder.InsertData(
                table: "Reviewers",
                columns: new[] { "Id", "IsActive", "PremiumReviewPrice", "ShortReviewPrice", "StandartReviewPrice", "UserId" },
                values: new object[,]
                {
                    { 1, true, 200m, 100m, 150m, "4d693871-c20b-4e9f-8490-1c641b9e3a40" },
                    { 2, true, 250m, 100m, 200m, "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d" }
                });

            migrationBuilder.InsertData(
                table: "Salesmen",
                columns: new[] { "Id", "IsActive", "SalesmanRating", "UserId" },
                values: new object[] { 1, true, 3, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "Description", "ImageUrl", "ImporterId", "KilometersDriven", "Model", "OwnerId", "Price", "SalesmanId", "TopSpeed", "VehicleType", "YearProduced" },
                values: new object[,]
                {
                    { 1, "BMW", "Fast car", "https://media.ed.edmunds-media.com/bmw/m5/2021/oem/2021_bmw_m5_sedan_base_fq_oem_8_815.jpg", null, 0.0, "M5", null, 9000m, 1, 250.0, 1, 2016 },
                    { 2, "Bugatti", "Fast sports car", "https://i.ytimg.com/vi/rvn4lHrr6AQ/maxresdefault.jpg", null, 0.0, "Veyron", null, 60000m, 1, 350.0, 1, 2011 },
                    { 3, "Audi", "Fast modern car", "https://cache1.24chasa.bg/Images/Cache/889/IMAGE_13981889_40_0.jpg", null, 0.0, "A6", null, 20000m, 1, 300.0, 1, 2014 },
                    { 4, "BMW", "Fast modern car", "https://media.ed.edmunds-media.com/bmw/m3/2022/oem/2022_bmw_m3_sedan_competition_fq_oem_1_1600.jpg", 1, 0.0, "M3", null, 10000m, null, 240.0, 1, 2004 },
                    { 5, "BMW", "Fast new car", "https://stimg.cardekho.com/images/carexteriorimages/930x620/BMW/X7/10571/1689673096346/front-left-side-47.jpg", 1, 0.0, "X7", null, 70000m, null, 300.0, 1, 2023 },
                    { 6, "BMW", "Popular car", "https://media.ed.edmunds-media.com/bmw/x5/2024/oem/2024_bmw_x5_4dr-suv_xdrive50e_fq_oem_1_1600.jpg", null, 2000.0, "X5", 1, 18000m, null, 243.0, 1, 2020 },
                    { 7, "Ford", "Old car", "https://media.autoexpress.co.uk/image/private/s--X-WVjvBW--/f_auto,t_content-image-full-desktop@1/v1562246965/autoexpress/2018/10/_dsf9821.jpg", null, 20000.0, "Escort", 3, 8000m, null, 160.0, 1, 1968 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Features", "Interior", "Longevity", "Overview", "Performance", "Price", "ReviewStatus", "ReviewType", "ReviewerId", "Title", "VehicleId", "VehicleRating" },
                values: new object[,]
                {
                    { 1, null, "The M5 has an elegant leather interior with supportive sport seats. Unlike many of its rivals, BMW hasn't gone the all-touchscreen route, so adjusting the air conditioning or radio (via physical controls) is easy to do while the vehicle is in motion. The M5 comes with a slew of desirable features such as customizable ambient interior lighting, a heated steering wheel, heated front seats, and a power-adjustable steering column. BMW offers ventilated front seats with massage functionality, heated rear seats, and four-zone automatic climate control for more coin. As for storage space, the M5 has useful cubbies in the cabin, and its trunk held six carry-on suitcases in our testing.", null, "Some cars are big-bodied and some are thrilling. The BMW M5 is both, with a body based on the regular 5-series and a heart-and-lung transplant courtesy of the brand's M performance division. Under the hood lives a spectacular 600-hp twin-turbo V-8 bolted to an eight-speed automatic transmission that powers all four wheels. An optional Competition package turns up the heat with 17 extra horsepower, a more soulful exhaust, stiffer suspension, and Competition badging and trim. That version rocketed to 60 mph in 2.8 seconds in our testing. Built to handle the rigors of mountain hairpins, blasts on the autobahn, and everyday life the M5 offers a premium experience with a penchant for fireworks. Unlike its closest competitor, the Cadillac CT5-V Blackwing, the Bimmer's stealthy packaging isn't offset by a thunderous exhaust but its impressive comfort and refinement make it among the best in the premium sports sedan segment.", "The M5 is mighty, boasting 600 horsepower from its twin-turbo 4.4-liter V-8 in base form. In the more track-focused Competition, that figure increases to 617 horses. Believing that the Competition's V-8 had even more power than that we took that model to a dynamometer, were our suspicions proved true. We've also strapped our testing gear to both the regular M5 as well as the Competition model. Both met our expectations with brutally quick acceleration, sports-car-like cornering grip, and amazing stopping power. Likewise, the Competition proved to be quicker than the regular M5 on Virginia International Raceway's Grand Course at the 2019 Lightning Lap competition. Driving enjoyment is maximized here with lively and direct steering and a well-controlled albeit stiff ride. That doesn't mean the M5 can't also do duty as a luxury sedan: In Comfort mode, it cruises placidly, the cabin whisper quiet.", 150m, 1, 1, 1, "Rocket-ship propulsion, rear-drive mode adds spirit, luxe, comfy cabin.", 1, 5 },
                    { 2, null, null, null, "Driving a Bugatti Veyron is like carrying a 14.6-foot-long open wallet that is spewing 50-dollar bills. Drivers rush up from behind, tailgating before swerving into either of the Veyron’s rear-three-quarter blind spots, where they hang ape-like out of windows to snap photos with their cell phones. They won’t leave, either, because they know the Bugatti, averaging 11 mpg, can’t go far without refueling and that its driver will soon need to take a minute to compose himself. And when you open the Veyron’s door to exit—a gymnastic feat that requires grabbing one of your own ankles to drag it across that huge, hot sill—you will be greeted by 5 to 15 persons wielding cameras and asking questions. If you’re wearing shorts or a skirt, here’s a tip: Wear underwear.", "The somewhat disappointing news is that despite accurate, nicely weighted steering and 1.00 g of skidpad grip, the car isn’t particularly nimble in the hills, where it is taxed by its 4486-pound heft. It feels more like a Benz SL63 AMG than, say, a BMW M3.\r\n\r\nThe Veyron’s weird shifter, which we named Klaatu, is as alien as the rest of the car. Push down for park. Push once to the right for drive. Twice to the right for sport mode. Left for neutral. Left and down for reverse. No matter where you shove it, it instantly returns to its original position, à la BMW turn signals. This is annoying, but resist the urge to abuse any gears. A new transmission costs $123,200. Speaking of abuse, within the 366-page hardcover owner’s manual, there are 190 boxed messages headlined “WARNING!”\r\n", 100m, 1, 0, 1, "Three good powertrain options, high-end cabin materials, cutting-edge infotainment tech.", 2, 4 },
                    { 3, "Audi offers an average standard warranty that when compared with other premium brands, looks pretty basic. Jaguar offers more value here, with longer warranties and five years of complimentary scheduled maintenance", "The A6's interior design is sleek, modern, and nicely put together from excellent-quality materials. Soft leather adorns the seats and armrests, rich-looking wood and nickel-finished metal trim is tastefully applied to the dash and doors, and the majority of the A6's secondary controls—climate, drive mode, etc.—are handled by a large touch-sensitive panel underneath the main infotainment display. A similar system is used in the A8 luxury sedan and the Q8 crossover, and despite our usual griping about the takeover of touchscreen controls, it works well and provides satisfying haptic feedback. A large trunk and easy-to-fold rear seatbacks make the A6 great for hauling cargo. We fit six of our carry-on suitcases in the trunk, which ties both the E450 and the 540i. The Audi offered far more space than either of those two with the rear seats folded, managing to hold 20 cases; the Benz held 18 and the BMW 16.", "Audi offers quite a few standard and optional driver-assistance features, including a system that watches out for traffic to save you from stepping out of the car and into the path of a moving vehicle. For more information about the A6's crash-test results, visit the National Highway Traffic Safety Administration (NHTSA) and Insurance Institute for Highway Safety (IIHS) websites.", "With its subdued styling and straightforward-but-refined interior, the 2024 Audi A6 is a classic German luxury sedan. There's nothing garish or overtly flashy about its design, and its comfort-first demeanor means it's perfect for long-haul autobahn runs. Entry-level models are powered by a turbocharged four-cylinder, but we like the optional turbocharged V-6 which makes a potent 335 horsepower. The more powerful S6 model (reviewed separately) livens things up with 444 horsepower and a tauter suspension. Driving enthusiasts may want to go with the S6 for more on-road entertainment, but those seeking quiet luxury will find that in the A6. Rivals such as the BMW 5-series, the Genesis G80, and the Mercedes-Benz E-Classserve as natural comparisons to the Audi and all offer similar style, comfort, and quality.", "The A6's two powertrains—a 261-hp turbocharged 2.0-liter four-cylinder and a 335-hp turbocharged 3.0-liter V-6—are both more than enough to haul this mid-size sedan around town without undue strain. Both powertrains employ hybrid technology with a 12- or 48-volt starter/alternator that runs the engine's stop-start system and other ancillary equipment. A seven-speed automatic transmission and Quattro all-wheel drive are both standard. The V-6 delivers plenty of thrust for merging and passing on the highway: at our test track, it charged from zero to 60 mph in just 4.8 seconds. Despite this quick result, it's not quite enough to outrun its key rivals, the BMW 540i xDrive and the Mercedes-Benz E450 4Matic. The 540i managed a 4.5-second run, while the Benz did it in 4.6. Thanks to its absorbent ride the A6 performs better as a luxury car than a sports sedan. We enjoyed its balanced handling and precise steering but never felt totally engaged when attacking twisty sections of road.", 200m, 1, 2, 1, "Impeccable interior furnishings, high-tech features integrated throughout, two perky turbo engine choices.", 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "Date", "ImporterId", "OwnerId", "SalePrice", "SalesmanId", "VehicleId", "VehiclePrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8368), 1, 3, 10000m, null, 1, 10000m },
                    { 2, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8435), null, 3, 0m, 1, 1, 10000m },
                    { 3, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8440), 1, 3, 60000m, null, 2, 60000m },
                    { 4, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8444), null, 3, 0m, 1, 2, 60000m },
                    { 5, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8448), 1, 3, 20000m, null, 3, 20000m },
                    { 6, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8453), null, 3, 0m, 1, 3, 20000m },
                    { 7, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8503), 1, 3, 18000m, null, 6, 18000m },
                    { 8, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8508), null, 3, 0m, 1, 6, 18000m },
                    { 9, new DateTime(2023, 11, 22, 12, 36, 48, 685, DateTimeKind.Local).AddTicks(8517), 1, 3, 8000m, null, 7, 8000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Importers_UserId",
                table: "Importers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferorId",
                table: "Offers",
                column: "OfferorId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SalesmanId",
                table: "Offers",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_VehicleId",
                table: "Offers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviewers_UserId",
                table: "Reviewers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_VehicleId",
                table: "Reviews",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRequests_RoleId",
                table: "RoleRequests",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRequests_UserId",
                table: "RoleRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ImporterId",
                table: "Sales",
                column: "ImporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OwnerId",
                table: "Sales",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalesmanId",
                table: "Sales",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_VehicleId",
                table: "Sales",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Salesmen_UserId",
                table: "Salesmen",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ImporterId",
                table: "Vehicles",
                column: "ImporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_SalesmanId",
                table: "Vehicles",
                column: "SalesmanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoleRequests");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Reviewers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Importers");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Salesmen");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9cbd5531-0c49-4889-95b9-b81fc1e7653a", "10933c11-ac2a-410d-b60a-8b1d97324975" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "10933c11-ac2a-410d-b60a-8b1d97324975" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2d1b88f7-208a-43de-bb14-aca56b43080c", "4d693871-c20b-4e9f-8490-1c641b9e3a40" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "4d693871-c20b-4e9f-8490-1c641b9e3a40" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c63016c0-e087-43dc-bb9c-a8958a05cbdd", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "926bee86-8bbd-43f6-bc1c-9639d43531a4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2d1b88f7-208a-43de-bb14-aca56b43080c", "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "b5fef437-f504-46d2-926d-3158e54e1932" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "cbed6d2a-e60a-49df-a6e3-982ccd980393" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dacb7d40-e742-435c-b131-145300f3c97d", "cbed6d2a-e60a-49df-a6e3-982ccd980393" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d1b88f7-208a-43de-bb14-aca56b43080c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cbd5531-0c49-4889-95b9-b81fc1e7653a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbea2448-c801-43d1-8b05-e3a2c22338d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c63016c0-e087-43dc-bb9c-a8958a05cbdd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dacb7d40-e742-435c-b131-145300f3c97d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10933c11-ac2a-410d-b60a-8b1d97324975");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d693871-c20b-4e9f-8490-1c641b9e3a40");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "926bee86-8bbd-43f6-bc1c-9639d43531a4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5fef437-f504-46d2-926d-3158e54e1932");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbed6d2a-e60a-49df-a6e3-982ccd980393");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
