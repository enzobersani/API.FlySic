using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.FlySic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightForm_Users_UserId",
                table: "FlightForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightForm",
                table: "FlightForm");

            migrationBuilder.RenameTable(
                name: "FlightForm",
                newName: "FlightForms");

            migrationBuilder.RenameIndex(
                name: "IX_FlightForm_UserId",
                table: "FlightForms",
                newName: "IX_FlightForms_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightForms",
                table: "FlightForms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightForms_Users_UserId",
                table: "FlightForms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightForms_Users_UserId",
                table: "FlightForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightForms",
                table: "FlightForms");

            migrationBuilder.RenameTable(
                name: "FlightForms",
                newName: "FlightForm");

            migrationBuilder.RenameIndex(
                name: "IX_FlightForms_UserId",
                table: "FlightForm",
                newName: "IX_FlightForm_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightForm",
                table: "FlightForm",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightForm_Users_UserId",
                table: "FlightForm",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
