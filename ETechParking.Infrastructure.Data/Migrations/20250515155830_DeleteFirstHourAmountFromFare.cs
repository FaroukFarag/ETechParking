using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETechParking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFirstHourAmountFromFare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "ClientTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "VIP");

            migrationBuilder.UpdateData(
                table: "FareTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "FareTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "VIP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
