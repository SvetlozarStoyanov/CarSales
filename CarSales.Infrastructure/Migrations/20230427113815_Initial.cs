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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bbea2448-c801-43d1-8b05-e3a2c22338d9", "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7bf1dd9-235e-4449-830c-184d73081541", "AQAAAAIAAYagAAAAEMTwAnR1nIURiTTgTY0L8Hh/cb5uKe21BsByf5+C0JQsRW+pKtIFRyJ31TwmBT/9iA==", "859c9d1d-3f06-4846-bc73-611db8dbb07e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c398881f-46ed-4969-9aba-9d9c1a5cd452", "AQAAAAIAAYagAAAAEKnKAQIk3Lo42B/sQ4QogM888d1/O8ZsyMO9ICxhdBCntdjfMYCA7LwtWjgTiJw9cw==", "ca3deeb2-f00a-471a-86a7-d8a0a2731cfe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5fef437-f504-46d2-926d-3158e54e1932",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "147f514a-ae32-4a60-81ee-4e4756a7d9db", "AQAAAAIAAYagAAAAEMakFTcWs4CyR9N4Eb1YfCZ4vW80LRpv6wyQl//A9E+GLCU1Akt/O7awG8b0UAtfcA==", "69914115-32d3-450e-9b9e-a6a968dbaaf5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc12dbbb-42f7-48c0-bd17-aa3d808871c8", "AQAAAAIAAYagAAAAECpc3SqcXtbqfReBdZnizPUNosHXbPi2NKN8W0iPHCXd+DSptxxnSambJ9B2HQCnUg==", "dbab16d1-b529-4887-8e93-0ce3290e6db3" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 2, "926bee86-8bbd-43f6-bc1c-9639d43531a4" },
                    { 3, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50" }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
