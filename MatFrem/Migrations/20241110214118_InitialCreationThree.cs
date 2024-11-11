using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreationThree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "820889af-1481-405f-8a47-b4622941d07a", "AQAAAAIAAYagAAAAEHCkPV4fSzxTQCHizKTg/7KTN1DMe2Ua0f9qVtz6HxJHcnojgOXIX9bLN2LA3iWhhQ==", "40748608", "af0d371c-a6d3-496c-899a-db5df154b11f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "6879228e-df0b-42f9-bd56-9badb112b52f", "AQAAAAIAAYagAAAAEGLdoFg712MM9kWP5vPhxhyELXSymAHm3DBtPzeOWgkA/xW+pe/z72d682xJfm4vcQ==", "95534356", "fdc4afe8-7948-4d24-8dd7-e97608d5e057" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "0819310a-45fb-46ac-ab20-3ac0aca91327", "AQAAAAIAAYagAAAAECW63POMUQcJy9gL6Rs1AXE9uXp4jqofxotEBhIL63maTK2J3ilcLBxvzdWQo6rckQ==", "43342364", "16181e79-cd6a-49e4-a0fa-1a010c178cef" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "2d52e169-a370-4a94-b571-92a30abace1f", "AQAAAAIAAYagAAAAEP1Is7fBM6nyneDU9Mg4bqT2lCbehBbwSxLQioM9eObepBWuK5SU5tpNVHowl2CrTA==", null, "14dc72a8-d375-4128-badc-b371e03ee176" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "049330b5-5b05-440b-a90e-0ee6f22187e0", "AQAAAAIAAYagAAAAECPZ0SzjEN52N0RRgdSCFSyNSWTGYQeWnnESco407wTYzLDZaJU14XZJ9zeuWFsM9w==", null, "cdfb4bf3-7b9d-496c-9183-85d311622356" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "f44e49f3-b3d7-420f-8ebb-859f03ee861b", "AQAAAAIAAYagAAAAEMA19ecpWXyLyGQXfSqWVaGB6uj5YBtHhvyzilfWJyr7FgIhTmRLUH9lllvmzJvIzA==", null, "d815523b-119a-41bc-b71c-b0bb3675636b" });
        }
    }
}
