using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auction.Migrations
{
    /// <inheritdoc />
    public partial class FieldsAddedInTournamentAndPlayerTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMarquee",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayerAvailability",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMarquee",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerAvailability",
                table: "Players");
        }
    }
}
