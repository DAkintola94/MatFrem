using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialAddTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_Id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderItems_OrderItemModelId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderItemModelId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderItemModelId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderModelOrderID1",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3873d29-8cb2-4247-b691-77df54f4db23", "AQAAAAIAAYagAAAAEEL4lXjC+TUuZiE7gjFcFdnRRnZnovv+W1giWgJYOOHhQSNlYTR6H+KVyYd+4oTpuQ==", "cdabeef2-8ffa-49a5-bbef-a5266e1187b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e8692cc-f8e0-4a3e-8480-af736946fc49", "AQAAAAIAAYagAAAAEFDUhMAKytlNESoCIsFPSizft4/5weOOWY4pMisH5OTjAZe69IbWMarxnTIOFJtt2w==", "1f5879c0-6c0e-494d-8ff7-407affb26899" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f700af95-d4ee-45bc-9efe-eded6021db30", "AQAAAAIAAYagAAAAEN7Wbg2r9TsjxUIihTGGG+mH9Dtx2DAcdNvYqK3icvNBOCcMvsohthv/d63RzakAaA==", "ca9b068b-a698-4aec-be13-6e04f8980e59" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderModelId",
                table: "OrderItems",
                column: "OrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderModelOrderID1",
                table: "OrderItems",
                column: "OrderModelOrderID1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderModelId",
                table: "OrderItems",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderModelOrderID1",
                table: "OrderItems",
                column: "OrderModelOrderID1",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderModelId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderModelOrderID1",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderModelId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderModelOrderID1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderModelOrderID1",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemModelId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50071894-d853-417f-bd87-e7d498be28bf", "AQAAAAIAAYagAAAAEPj6opaR27VladEVaHRM1e0v5hH3tWvqa5Ysikz+5zrI+bYeyAPCq1fzCjM8rgg+jA==", "71790f57-2517-4d93-9a79-5d45a386a497" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e25b27f6-1386-4963-a9b0-b403061ba89f", "AQAAAAIAAYagAAAAEHrkEaFYeell+vyMEtqhgWex+w9W4yejhnicZplKd1FyArUt5RAb2AhBVaorR0pg0Q==", "1ca8aff8-6b49-43d9-bf53-e5d66150b03b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5bfe9f84-c73d-4ea1-9b7c-c2a32a262272", "AQAAAAIAAYagAAAAEGDUIK4jdSQR8/02lmRH2caqU89j6jwA8uo6EhJzM3/jlal/inIiiNC0CXW0oHQrLQ==", "06908fd3-e637-407e-b25d-5635266d65a3" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderItemModelId",
                table: "Orders",
                column: "OrderItemModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_Id",
                table: "OrderItems",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderItems_OrderItemModelId",
                table: "Orders",
                column: "OrderItemModelId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }
    }
}
