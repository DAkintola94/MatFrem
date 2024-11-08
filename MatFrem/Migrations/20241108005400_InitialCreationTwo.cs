using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreationTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Customers_CustomerID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CustomerID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Locations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acaebc41-1be7-4f4d-bddc-ee22b10246dd", "AQAAAAIAAYagAAAAEGlc2XYzNY5NCnPILCJ61NiDANFHHUKU7NxKF2+824oihuvTqupTgEx70E2F9w5mMw==", "3f82d5a4-0b9f-424c-8e4a-f25a67149f3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73e61370-d5de-447a-8255-d07047d70361", "AQAAAAIAAYagAAAAEGt4iM8M7NLgWO4tj9/aUq5coPPNNWhZ3R/t4k35WJe95N3oHHQ6UgR2/+A+l2BJJQ==", "a696f47d-aec5-4997-aa76-56da7a141129" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eeaadcfe-e56b-4f49-b2d5-3889a4cae69c", "AQAAAAIAAYagAAAAEIlVWUxsDJFgr5UyoyXFO0k++ksq3h9+XoqnMY1jAsllsd2aIstriOYz6awJ0rdoqQ==", "c77136ab-a522-4dc1-90fc-1567ff926fef" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CustomerID",
                table: "Locations",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Customers_CustomerID",
                table: "Locations",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
