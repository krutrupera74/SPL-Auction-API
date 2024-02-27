using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auction.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Teams",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Teams",
                newName: "LogoUrl");
        }
    }
}
