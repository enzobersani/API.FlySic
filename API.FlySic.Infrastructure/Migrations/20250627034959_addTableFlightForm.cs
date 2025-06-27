using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.FlySic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTableFlightForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DepartureAirport = table.Column<string>(type: "text", nullable: true),
                    DepartureManualLocation = table.Column<string>(type: "text", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArrivalAirport = table.Column<string>(type: "text", nullable: true),
                    ArrivalManualLocation = table.Column<string>(type: "text", nullable: true),
                    AircraftType = table.Column<string>(type: "text", nullable: false),
                    FlightComment = table.Column<string>(type: "text", nullable: true),
                    HasOvernight = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightForm_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightForm_UserId",
                table: "FlightForm",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightForm");
        }
    }
}
