using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wprawka_1.Migrations
{
    /// <inheritdoc />
    public partial class RenameClusterNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClusterName",
                table: "Clusters",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Clusters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Klaster Północ" },
                    { 2, "Klaster Południe" },
                    { 3, "Klaster Wschód" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, "jan@wprawka.pl", "Jan Kowalski" },
                    { 2, "anna@nowak.pl", "Anna Nowak" }
                });

            migrationBuilder.InsertData(
                table: "Homes",
                columns: new[] { "Id", "Address", "UserId" },
                values: new object[,]
                {
                    { 1, "ul. Wiejska 45, Białystok", 1 },
                    { 2, "ul. Mazowiecka 10, Warszawa", 1 },
                    { 3, "ul. Lipowa 1, Białystok", 2 }
                });

            migrationBuilder.InsertData(
                table: "UserClusterMemberships",
                columns: new[] { "ClustersId", "UsersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clusters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserClusterMemberships",
                keyColumns: new[] { "ClustersId", "UsersId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserClusterMemberships",
                keyColumns: new[] { "ClustersId", "UsersId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "UserClusterMemberships",
                keyColumns: new[] { "ClustersId", "UsersId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Clusters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clusters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clusters",
                newName: "ClusterName");
        }
    }
}
