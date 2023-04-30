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
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Salesmen",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88b91544-538e-4b8a-84d2-35e62bf32de2", "AQAAAAIAAYagAAAAEFv6wcI6HdN7zfZSfB2dRQ5B+DLBWztUfA0VHhjn1IjEGEa0CLniBAYkrv9Rw+QP/Q==", "63eca998-dbce-44b3-bb29-82b42bb24e27" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32342018-d1ca-4afb-ac40-19a596ff7de7", "AQAAAAIAAYagAAAAEH1TFipE7N1SCY3zIB5kzOkgPxR6z89dizGzYvye3TInTAPgvCIoye4loqjs1VGRwQ==", "9b2a5058-0121-4173-be53-372d7ba1a780" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5fef437-f504-46d2-926d-3158e54e1932",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "308c3470-8607-46a2-b5d3-9bf19ce628fc", "AQAAAAIAAYagAAAAECcNy6aprgv4Y7LReTRw7uaVFYHApUaEnkx5DybtqLhC4K6FZK4JhPXq6j1LO4gfzw==", "25b51e02-320a-4a79-ad4c-ed5fca145216" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8aa5bbd9-8179-45a7-8303-d299c3986f01", "AQAAAAIAAYagAAAAEKiGQhcfyaASUyV+IvIxbMU4n8O30fpGe05iKKVjpsH06xkk42NsPh7FRf/XUHOwxA==", "fb19cdbc-1ec9-443e-a23a-43849223dbef" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 2, "926bee86-8bbd-43f6-bc1c-9639d43531a4" },
                    { 3, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" }
                });

            migrationBuilder.UpdateData(
                table: "Salesmen",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://media.ed.edmunds-media.com/bmw/m5/2021/oem/2021_bmw_m5_sedan_base_fq_oem_8_815.jpg");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_RoleRequests_RoleId",
                table: "RoleRequests",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRequests_UserId",
                table: "RoleRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleRequests");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" });

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Salesmen");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3bc7cac9-a1e9-49e7-9d53-821277999f75", "AQAAAAIAAYagAAAAEJ5ZzzwKK086LMMBQ8aQ+rq+cbosdiwhmk285WrMCDpRXs5ea4UZbtQcvafds1DiLA==", "543be2a1-d0dc-46e0-a2f9-665af505bbf3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07581444-301f-4b13-b205-e005fba7803c", "AQAAAAIAAYagAAAAEJF8w0wYC9LlCAad8pD8zMqoQdgFIjoQ/m1moY4yJK4OTSIf3MFY0tojpt5ux9PvMQ==", "c51bdf0a-25e0-44c6-9f45-3d2a960bb9ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5fef437-f504-46d2-926d-3158e54e1932",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50236cc1-5f3a-4330-b5c3-e6cbfd48bae4", "AQAAAAIAAYagAAAAEFjGJZN8Y5m3Sx0uIHxlmjPyeDq1yXQPAnLj3GyHw0hgWzVkET5vyzHx15TZTmfCXw==", "5d2c6176-e88d-444d-a2e5-5c80faa12b40" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3fbde61-1c9a-426b-ad87-2ed58b859b78", "AQAAAAIAAYagAAAAEIl6zinUsWsv05Iba3zLGijrxVFeXvRzG39Kp3izwDI48lM6/Pyz7bDZUsapeJB52g==", "e08a71cd-4c61-4705-a67d-51679c84fd2f" });
        }
    }
}
