using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auction.Migrations
{
    /// <inheritdoc />
    public partial class Changedplayertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattingRating",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BowlingRating",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "WicketKeepingRating",
                table: "Players",
                newName: "PlayerRating");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Players",
                newName: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "Players",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "PlayerRating",
                table: "Players",
                newName: "WicketKeepingRating");

            migrationBuilder.AddColumn<int>(
                name: "BattingRating",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BowlingRating",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
