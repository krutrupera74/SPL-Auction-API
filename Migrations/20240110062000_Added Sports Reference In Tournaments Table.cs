using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auction.Migrations
{
    /// <inheritdoc />
    public partial class AddedSportsReferenceInTournamentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Tournaments");
        }
    }
}
