using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.FlySic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newFieldStatusTableFlightForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FlightForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "FlightForms");
        }
    }
}
