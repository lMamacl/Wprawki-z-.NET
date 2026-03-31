using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wprawka_2.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ClusterId", "DeviceName", "PowerWatt" },
                values: new object[] { 2, 2, "Pompa Ciepła Beta", 2500 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
