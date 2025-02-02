using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETechParking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaxLimitToFares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxLimit",
                table: "Fares",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxLimit",
                table: "Fares");
        }
    }
}
