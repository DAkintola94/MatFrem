using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialDesktop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product_detail",
                keyColumn: "ProductLocation",
                keyValue: null,
                column: "ProductLocation",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductLocation",
                table: "Product_detail",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductLocation",
                table: "Product_detail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18e0a565-a96f-43f2-8132-9297aff9d815", "AQAAAAIAAYagAAAAEC0uGjT067pr0QrmPPvGxkQP3U2udcRmsDTeMC2tpTa2BzB5XD8QGVt1qPI9kc0Szw==", "27cd94f4-e293-4a72-b9dc-b6d7acc3c3db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9963f9c8-2a75-42e3-b609-4fa0aa797f84", "AQAAAAIAAYagAAAAECFRTfdNoOAgTHHu5phUOPm5ekMen/zhiHGFpuEcnwwcqXXieJqjHrxVT1gpHFJsVw==", "2f6de3f5-5d49-4186-b4f5-2f99171efa9e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9294c65-5035-4ccf-9edb-2ae4f738f97c", "AQAAAAIAAYagAAAAEEEvN3pSnysseTcpOrAvUiG8fegjVb9IzOPKEI46zLOQSUQ8WZJI9Qp/8ngLPQ0pzg==", "5079475b-cda3-4ddc-a3fd-8b9756282e07" });
        }
    }
}
