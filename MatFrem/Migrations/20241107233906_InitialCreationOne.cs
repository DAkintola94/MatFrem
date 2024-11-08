using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreationOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Locations",
                newName: "LocationReportID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41546c99-55c0-49ed-b94d-85b2fa00849f", "AQAAAAIAAYagAAAAEM3JHCBiKUG1mpyhdvoOPvBcnsGolIZCu2bxglsczg9ZT23FW+jQ6gPQm3Wyd5vZBg==", "1d902bea-2c10-4b82-be60-5f2377d8fff7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bd26743-cbdd-48ab-8c38-2f5446d4cc8b", "AQAAAAIAAYagAAAAEJFOm05dJeG+6Y6rRNniTWWGBR27pndk9b62sWfiLmPHtjCvv+anuy+L+xMrQxV+SQ==", "d746e59a-0409-4490-8d95-8d3c19ae0baf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0776569-8955-4382-bab2-c939e4912c64", "AQAAAAIAAYagAAAAEH62iZinTM83milzvBR57KqltQzwKbx8sKOddtITaR8qXwfGluQM+CO2VVPWoBbiiQ==", "9775353c-6310-4355-8780-3302e31af45c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationReportID",
                table: "Locations",
                newName: "LocationID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20925b3f-654c-45c6-883d-80f8ec9e5f93", "AQAAAAIAAYagAAAAEJboMOjaklFy03vzsW1IO1Kj9YyWNxWW3buXH1agGR3FtM3WxoU0IPBn9DnwFQuevg==", "f927914a-89c1-42ee-95e9-9b1b05844267" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42611e6d-a971-40ff-8a20-7c6daf934235", "AQAAAAIAAYagAAAAEJDKGaxYBbX8kBbg0FQqZ6AiOg8wA3m4A3ytU+pB1zExXz//F5temQx8NwW3nOQVVw==", "64b32519-8d40-4a84-879e-04d54026bacb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "784c2807-2ae7-46a0-914c-54aa46186d02", "AQAAAAIAAYagAAAAEKNnAVkQrI7hFjOtzA/nGbETomxKnSAce4WIBjYcy62ju7BNE2wCCXcyglySaP7veA==", "9cac32d0-b2e1-4b96-81a5-4058589260ce" });
        }
    }
}
