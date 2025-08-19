using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.FlySic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newFieldAcceptedUserTableFlightForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightForms_Users_UserId",
                table: "FlightForms");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FlightForms",
                newName: "PilotId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightForms_UserId",
                table: "FlightForms",
                newName: "IX_FlightForms_PilotId");

            migrationBuilder.AddColumn<Guid>(
                name: "AcceptedUserId",
                table: "FlightForms",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightForms_AcceptedUserId",
                table: "FlightForms",
                column: "AcceptedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightForms_Users_AcceptedUserId",
                table: "FlightForms",
                column: "AcceptedUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightForms_Users_PilotId",
                table: "FlightForms",
                column: "PilotId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightForms_Users_AcceptedUserId",
                table: "FlightForms");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightForms_Users_PilotId",
                table: "FlightForms");

            migrationBuilder.DropIndex(
                name: "IX_FlightForms_AcceptedUserId",
                table: "FlightForms");

            migrationBuilder.DropColumn(
                name: "AcceptedUserId",
                table: "FlightForms");

            migrationBuilder.RenameColumn(
                name: "PilotId",
                table: "FlightForms",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightForms_PilotId",
                table: "FlightForms",
                newName: "IX_FlightForms_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightForms_Users_UserId",
                table: "FlightForms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
