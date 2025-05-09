using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETechParking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstHourAmountToFare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FirstHourAmount",
                table: "Fares",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Car");

            migrationBuilder.UpdateData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Bus");

            migrationBuilder.UpdateData(
                table: "FareTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Car");

            migrationBuilder.UpdateData(
                table: "FareTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Bus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstHourAmount",
                table: "Fares");

            migrationBuilder.UpdateData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Visitor");

            migrationBuilder.UpdateData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Guest");

            migrationBuilder.UpdateData(
                table: "FareTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Hourly");

            migrationBuilder.UpdateData(
                table: "FareTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Daily");
        }
    }
}
