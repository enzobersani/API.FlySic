using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.FlySic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addFieldIsFirstAccessOnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAccess",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstAccess",
                table: "Users");
        }
    }
}
