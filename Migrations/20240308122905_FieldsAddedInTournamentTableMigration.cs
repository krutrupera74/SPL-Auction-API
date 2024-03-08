using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auction.Migrations
{
    /// <inheritdoc />
    public partial class FieldsAddedInTournamentTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TournamentDates",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentDates",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Tournaments");
        }
    }
}
