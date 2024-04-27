using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSharingApi.Migrations
{
    /// <inheritdoc />
    public partial class ConstraintUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoCategories_Photos_photo_id",
                table: "PhotoCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoCategories_Photos_photo_id",
                table: "PhotoCategories",
                column: "photo_id",
                principalTable: "Photos",
                principalColumn: "photo_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoCategories_Photos_photo_id",
                table: "PhotoCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoCategories_Photos_photo_id",
                table: "PhotoCategories",
                column: "photo_id",
                principalTable: "Photos",
                principalColumn: "photo_id");
        }
    }
}
