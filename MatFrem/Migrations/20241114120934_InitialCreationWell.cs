using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatFrem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreationWell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "279c8d80-fe5b-46b8-a150-8b791e7ffa9f", "AQAAAAIAAYagAAAAEEoKYMVW7dm7VD9QxGOeTdtl/PUw26eY76BNyRmpT0c8JOm7jGRx3wJ/PpoX2sDnGQ==", "be74abcf-f701-47c3-a728-230ac8c023c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e10b1e2-eb20-4900-b598-525308659f11", "AQAAAAIAAYagAAAAEE1FIrxvOLMzG8iodZmyvOpQQGGcEjUdAFcJQI617LIwBQdQ1HN2gt8/jrusHccojw==", "19d63c36-d8ad-46c6-87c6-62b4f76b7531" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89f57490-b2b9-4263-a144-a54ef837b631", "AQAAAAIAAYagAAAAEBfOzS+CVVmWon4ry7Pt5cix1P0rRrUrdvC2FgZeQ4FGe10mOuZ4dzq+vzkEoqVBrg==", "a83d2dd0-a106-439d-a780-41b5f6ab57d0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
