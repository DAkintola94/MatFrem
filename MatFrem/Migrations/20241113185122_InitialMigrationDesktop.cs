using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationDesktop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advice",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdviceMessage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advice", x => x.PostId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "110483ca-2b92-4af0-a6db-217585cb86a4", "AQAAAAIAAYagAAAAEK2kN0ESAXPEjOHLxNTvLAaQA6GKwu+YFnMaiz+QnMMmfu4lhFsUujJ6Y8hrdlSD8w==", "adb46b11-6784-47af-8039-454add60c31a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51ba9810-acd8-4ec4-b69d-0e98fbe6d2c0", "AQAAAAIAAYagAAAAEHqaERrosf/h3Bwa4OEUpQD5+2lsxykv8dOE6d2fzYQY20+svjxE+kcHdg3A625zuA==", "1cadc1ff-5a3d-4e8b-bb51-ec80f97def91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8ae4a25-2848-4ce6-ba5b-e3c840af5467", "AQAAAAIAAYagAAAAEFEo/PuNF7j9LSqbCam9Jfu1W35s59cxKJqpeOROcr4d/v+FrqZ1Rlj/2XQv+ziETg==", "ecc65010-31ff-462a-9ab8-567fd90d9738" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advice");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1d91a3d-54d1-4762-a01b-76925209092b", "AQAAAAIAAYagAAAAEIxpwPhctqM1jrzAlIivXGxBrE/GeaLu1xPrG9euOLTRmXYTcqJ6MIf8PheRbkbv9A==", "8c1c1b95-a849-4026-8e45-7887b5017c9e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36ebbae0-fe30-4257-86b2-2296d9ae2b2e", "AQAAAAIAAYagAAAAEExMwBg3y1DIe5kEoLx9g7NchJL2qeiVDRfOAVGW7prxznmrKf0X6xlDyeV0dCI04g==", "0705ec4c-b6eb-4bae-998c-070357a3acbf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d1505ca-8da8-48dc-9f16-325e3bea748e", "AQAAAAIAAYagAAAAELA1drNlpRiHn+FgxqoBt8bv5xrpAnpUHBx673DfKjjyshFsj1/c/C+pYUvAfKXe5A==", "a3c881f8-45b8-4111-b8f3-fec2c7756c8a" });
        }
    }
}
