using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreationLaptop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderItem",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59cc81cc-4442-4312-a65e-52c3155f35d4", "AQAAAAIAAYagAAAAEKT/zP/Ztb7j9BCH7TXYBu3FDg6CliI4PIcDL8O21EIuKPwujJKK9AQ4Un15RY1Keg==", "8f23e3f6-a767-46c1-8e31-3c0f9d6b78a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29cc20d4-7d21-4230-8181-0c88502a0524", "AQAAAAIAAYagAAAAEAEZ1NhVWmhHOiyflukiOC+pwLZmRqLcnPyqHNI4cs+cS7V00WI/mvL47Dcv6IGlxQ==", "e7af6604-8005-43b8-8f2d-08f3ec7760aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8c8a9c8-9a5a-4d3a-87d5-551444687313", "AQAAAAIAAYagAAAAEDshjB29G7tDqfMdWlwM9Ixh7sGnw+T25dOSfZOlJ4mGTQsnLsuJMM2lOOl6I1veVw==", "53e867d2-9316-4887-9542-391163e89596" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderItem",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02fead36-978a-4073-a860-8dfc0863513d", "AQAAAAIAAYagAAAAEMpzUeFUuVMKPPkouZ71oeJgIqBIoNQq4QOKg2LR/Ns8Lscq51s0VHY5BzLgoc4jcQ==", "e6108782-f8f3-4976-9a4f-5409b9af4ca1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e800f63-1e67-4680-bbcb-40c3a36734cd", "AQAAAAIAAYagAAAAEFqbRRSGNTD5ShhQUhLX1qT2SDLr1o526RL1dSNCfI+GRPRH7tOxE/hT02Zze4IfBQ==", "75aab215-f7f1-4d73-810f-03b4df91b66b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "816e354c-f809-4c3b-9c88-da136acae029", "AQAAAAIAAYagAAAAEDons3q2Ls1fRjfNxewA2h6jwnRjJJK8FtnHDwSvBj/PGmxbRj7rfXycbV7mTUMuPg==", "3c67d80b-d29e-4d76-8521-9dbcc4adbf88" });
        }
    }
}
