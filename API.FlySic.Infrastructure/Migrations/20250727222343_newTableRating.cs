using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.FlySic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newTableRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlightFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightRatings_FlightForms_FlightFormId",
                        column: x => x.FlightFormId,
                        principalTable: "FlightForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightRatings_Users_EvaluatedId",
                        column: x => x.EvaluatedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightRatings_Users_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightRatings_EvaluatedId",
                table: "FlightRatings",
                column: "EvaluatedId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRatings_EvaluatorId",
                table: "FlightRatings",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRatings_FlightFormId",
                table: "FlightRatings",
                column: "FlightFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightRatings");
        }
    }
}
