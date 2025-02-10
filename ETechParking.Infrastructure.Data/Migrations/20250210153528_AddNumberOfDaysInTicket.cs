using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETechParking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfDaysInTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "Tickets",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Tickets");
        }
    }
}
