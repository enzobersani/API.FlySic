using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.FlySic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newTableFlightFormInterest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightFormInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlightFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    InterestedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightFormInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightFormInterests_FlightForms_FlightFormId",
                        column: x => x.FlightFormId,
                        principalTable: "FlightForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightFormInterests_Users_InterestedUserId",
                        column: x => x.InterestedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightFormInterests_FlightFormId",
                table: "FlightFormInterests",
                column: "FlightFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightFormInterests_InterestedUserId",
                table: "FlightFormInterests",
                column: "InterestedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightFormInterests");
        }
    }
}
