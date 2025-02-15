using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETechParking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateAndCloseUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CloseUserId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateUserId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CloseUserId",
                table: "Tickets",
                column: "CloseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreateUserId",
                table: "Tickets",
                column: "CreateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CloseUserId",
                table: "Tickets",
                column: "CloseUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CreateUserId",
                table: "Tickets",
                column: "CreateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CloseUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CreateUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CloseUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CreateUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CloseUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Tickets");
        }
    }
}
