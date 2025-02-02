using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETechParking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnterGracePeriod",
                table: "Fares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExitGracePeriod",
                table: "Fares",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnterGracePeriod",
                table: "Fares");

            migrationBuilder.DropColumn(
                name: "ExitGracePeriod",
                table: "Fares");
        }
    }
}
