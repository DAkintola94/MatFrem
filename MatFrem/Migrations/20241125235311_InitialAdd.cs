using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderState_Orders_OrderStatusID",
                table: "OrderState");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_detail_Shop_detail_ShopModelShopID",
                table: "Product_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Shop_detail_Locations_LocationID",
                table: "Shop_detail");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Shop_detail_LocationID",
                table: "Shop_detail");

            migrationBuilder.DropIndex(
                name: "IX_Product_detail_ShopModelShopID",
                table: "Product_detail");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Shop_detail");

            migrationBuilder.DropColumn(
                name: "ShopModelShopID",
                table: "Product_detail");

            migrationBuilder.DropColumn(
                name: "OrderItemID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderStatusID",
                table: "Orders",
                newName: "OrderItem");

            migrationBuilder.AddColumn<int>(
                name: "CartSize",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusID",
                table: "OrderState",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryFee",
                table: "Orders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemModelId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Order_Status",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProductMProductID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "ProductPrice",
                table: "Orders",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderModelOrderID",
                table: "OrderItems",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductMProductID",
                table: "Orders",
                column: "ProductMProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderModelOrderID",
                table: "OrderItems",
                column: "OrderModelOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderModelOrderID",
                table: "OrderItems",
                column: "OrderModelOrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderItems_OrderItemModelId",
                table: "Orders",
                column: "OrderItemModelId",
                principalTable: "OrderItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Product_detail_ProductMProductID",
                table: "Orders",
                column: "ProductMProductID",
                principalTable: "Product_detail",
                principalColumn: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderModelOrderID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderItems_OrderItemModelId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Product_detail_ProductMProductID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderItemModelId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductMProductID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderModelOrderID",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CartSize",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderItemModelId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order_Status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductMProductID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderModelOrderID",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderItem",
                table: "Orders",
                newName: "OrderStatusID");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationID",
                table: "Shop_detail",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "ShopModelShopID",
                table: "Product_detail",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusID",
                table: "OrderState",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "CustomerName",
                keyValue: null,
                column: "CustomerName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationReportID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrderModelsOrderID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    GeoJson = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationMessage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShopLocationID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationReportID);
                    table.ForeignKey(
                        name: "FK_Locations_Orders_OrderModelsOrderID",
                        column: x => x.OrderModelsOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e344c7c-988e-4e86-a082-4ab11cf8b79c", "AQAAAAIAAYagAAAAEIeCJoKJUplFtQbu7qExOSNSq8CaDePl46u43JgMvzjCwdY1rBBywaJVnseyttPniw==", "5d160196-2a79-42c9-bd79-32e037b9acf5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a65c3ba1-3799-4b65-bca7-d599e90fa662", "AQAAAAIAAYagAAAAEMzOlGqYrCemnNfqt9w4dDnRfto2EGBI4CkdJ39o3zhX/6TtnvxGqynt+Pl850Ihvw==", "e5121931-f3ed-4191-aaea-b09b8bb48356" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5aea34a1-10aa-4298-b03e-ba7fb7d06cca", "AQAAAAIAAYagAAAAEJRyBPxA9sEMexMsX3ARL9J7majoh6EI5/cD9tpA67UEiHwmBcOzrXPnSMHwbe6TgA==", "889d2be7-f68c-4129-a403-83ea1528b777" });

            migrationBuilder.CreateIndex(
                name: "IX_Shop_detail_LocationID",
                table: "Shop_detail",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_detail_ShopModelShopID",
                table: "Product_detail",
                column: "ShopModelShopID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_OrderModelsOrderID",
                table: "Locations",
                column: "OrderModelsOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderState_Orders_OrderStatusID",
                table: "OrderState",
                column: "OrderStatusID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_detail_Shop_detail_ShopModelShopID",
                table: "Product_detail",
                column: "ShopModelShopID",
                principalTable: "Shop_detail",
                principalColumn: "ShopID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_detail_Locations_LocationID",
                table: "Shop_detail",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "LocationReportID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
