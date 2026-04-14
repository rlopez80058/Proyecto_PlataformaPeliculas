using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFavoriteFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "LibraryItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "LibraryItems");
        }
    }
}
