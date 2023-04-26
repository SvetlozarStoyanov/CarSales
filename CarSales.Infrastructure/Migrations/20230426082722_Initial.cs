using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd715f14-0b77-4066-828a-48adba54a22f", "AQAAAAIAAYagAAAAEEN7LlzNTwoEpHO8NSNL2m8/PUC7Q3IK+u5SaAV2tWbYMTJknhZsarrHBDLqbFjNnQ==", "193f47c0-f911-44e7-9837-bcd4af239d04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ace843c-3b69-4eac-956f-c74e7339f252", "AQAAAAIAAYagAAAAED9UoymaGbL1F0JgfzjoFHCaRlHEKVftU3rP48wN2E4Ao4joLslJpb8jMJjInaoxig==", "c7e4a1b0-c2db-498d-87b5-89e753c17e82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5fef437-f504-46d2-926d-3158e54e1932",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83480018-ecf3-4ffa-bee7-22bb03f8cf2a", "AQAAAAIAAYagAAAAEN1UbuU15HaEKMHqJl1ty9rzNDabO/yGcA6bdk4q4M8SEUHBjyF87omXa3gMBNoyFA==", "90353409-5069-4f61-87b3-ffdea6765189" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "436d439e-c130-44cd-9b6e-7cb11641734b", "AQAAAAIAAYagAAAAEA6NkVQXJtgnqpyTFYsFjuWZbnAWnZTAe6eK2BTuPacrYgf9yH+3e71yoPhs9NXi/g==", "20bfe686-f6ca-46cd-b7b4-23baab59b402" });

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
