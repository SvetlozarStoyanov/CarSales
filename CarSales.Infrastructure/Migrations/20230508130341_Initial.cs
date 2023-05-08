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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Importers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Importers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleRequests_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salesmen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalesmanRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesmen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salesmen_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    VehicleRating = table.Column<int>(type: "int", nullable: false),
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
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesmanId = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    { "9cbd5531-0c49-4889-95b9-b81fc1e7653a", "9cbd5531-0c49-4889-95b9-b81fc1e7653a", "Imports vehicles.", "Role", "Importer", "IMPORTER" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "bbea2448-c801-43d1-8b05-e3a2c22338d9", "Can buy vehicles.", "Role", "Owner", "OWNER" },
                    { "c63016c0-e087-43dc-bb9c-a8958a05cbdd", "c63016c0-e087-43dc-bb9c-a8958a05cbdd", "Can buy and sell vehicles.", "Role", "Salesman", "SALESMAN" },
                    { "dacb7d40-e742-435c-b131-145300f3c97d", "dacb7d40-e742-435c-b131-145300f3c97d", "Admin", "Role", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Credits", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "10933c11-ac2a-410d-b60a-8b1d97324975", 0, "0a3e56de-ec35-4b01-87e9-10049de3efbb", 50000m, "Importer@gmail.com", false, "Importer", "Test", false, null, "IMPORTER@GMAIL.COM", "IMPORTER", "AQAAAAIAAYagAAAAEJ8TqtM59hn06HRsU+0NEOVfVHXREsMuiu3/FfrMpzsQx/nOpOuQRRofZrwjzmrL/w==", null, false, "5d818ece-5ed1-4e3e-ba18-60fe89ebb0a1", false, "Importer" },
                    { "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", 0, "666b4e38-173a-4628-b0ea-c4ff741ee438", 50000m, "Salesman@gmail.com", false, "Salesman", "Test", false, null, "SALESMAN@GMAIL.COM", "SALESMAN", "AQAAAAIAAYagAAAAEMzwjBfTcmh8OxEUiX7PbWfwYuKiZnyzcTkuqaTMRScbLXb1ZEt1vcGBpD7Xe5Ahqg==", null, false, "05ce33ba-0da4-42c9-8695-c608ee7533b7", false, "Salesman" },
                    { "926bee86-8bbd-43f6-bc1c-9639d43531a4", 0, "6fa25b91-01d8-4d80-83db-8367102e8901", 50000m, "Owner2@gmail.com", false, "Owner2", "Test", false, null, "OWNER2@GMAIL.COM", "OWNER2", "AQAAAAIAAYagAAAAEE0V35tdvOfMj29GPXOBl09jL55FGyrolejEJovGO45C7UcTq7qAqD6j2q7RTi8EpQ==", null, false, "b2845bd0-100f-44a4-8183-513bc2779874", false, "Owner2" },
                    { "b5fef437-f504-46d2-926d-3158e54e1932", 0, "cf4237ec-5567-4cb3-ba84-699247a1e5d1", 50000m, "Owner@gmail.com", false, "Owner", "Test", false, null, "OWNER@GMAIL.COM", "OWNER", "AQAAAAIAAYagAAAAEAFRvXJxWeOmlLBc5jE21UzOI9owRVzt5jpNHUEbW1k5MKg3dIIT9MOWvJm6QJkr7Q==", null, false, "b7e276fd-38e7-43e4-857d-d1623217f067", false, "Owner" },
                    { "cbed6d2a-e60a-49df-a6e3-982ccd980393", 0, "407c42ac-4f5f-4b23-b417-403c751dfdce", 50000m, "Admin@gmail.com", false, "Admin", "Test", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAmgiGSA8OSUKxl97RY34sPOiPEHyZ9txjM7ou0WTL8pWhN3EHbIH+hK6T5f4mkXJg==", null, false, "7ea07e7e-6763-4088-9fbb-0988af81eaed", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9cbd5531-0c49-4889-95b9-b81fc1e7653a", "10933c11-ac2a-410d-b60a-8b1d97324975" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "10933c11-ac2a-410d-b60a-8b1d97324975" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" },
                    { "c63016c0-e087-43dc-bb9c-a8958a05cbdd", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "926bee86-8bbd-43f6-bc1c-9639d43531a4" },
                    { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "b5fef437-f504-46d2-926d-3158e54e1932" },
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
                    { 4, "10933c11-ac2a-410d-b60a-8b1d97324975" }
                });

            migrationBuilder.InsertData(
                table: "Salesmen",
                columns: new[] { "Id", "IsActive", "SalesmanRating", "UserId" },
                values: new object[] { 1, true, 3, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "Description", "ImageUrl", "ImporterId", "KilometersDriven", "Model", "OwnerId", "Price", "SalesmanId", "TopSpeed", "VehicleRating", "VehicleType", "YearProduced" },
                values: new object[,]
                {
                    { 1, "BMW", "Fast car", "https://media.ed.edmunds-media.com/bmw/m5/2021/oem/2021_bmw_m5_sedan_base_fq_oem_8_815.jpg", null, 0.0, "M5", null, 9000m, 1, 250.0, 3, 1, 2016 },
                    { 2, "BMW", "Classic car", null, 1, 0.0, "M3", null, 5000m, null, 240.0, 3, 1, 2004 },
                    { 3, "BMW", "Popular car", null, null, 2000.0, "X5", 1, 18000m, null, 243.0, 4, 1, 2020 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "ImporterId", "OwnerId", "SalePrice", "SalesmanId", "VehicleId" },
                values: new object[,]
                {
                    { 1, 1, null, 10000m, 1, 1 },
                    { 2, null, 1, 20000m, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Importers_UserId",
                table: "Importers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

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
                name: "RoleRequests");

            migrationBuilder.DropTable(
                name: "Sales");

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
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "b5fef437-f504-46d2-926d-3158e54e1932" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dacb7d40-e742-435c-b131-145300f3c97d", "cbed6d2a-e60a-49df-a6e3-982ccd980393" });

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
                keyValue: "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "926bee86-8bbd-43f6-bc1c-9639d43531a4");

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
