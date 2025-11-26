using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHS_Benjamin_Pasic.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsFavoriteFromNewsItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "NewsItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "NewsItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
