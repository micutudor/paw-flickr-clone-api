using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoSharingApi.Migrations
{
    /// <inheritdoc />
    public partial class Add_all_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "user_id");

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    album_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.album_id);
                    table.ForeignKey(
                        name: "FK_Albums_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    photo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    posted_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    geolocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.photo_id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    commented_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    photo_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_Comments_Photos_photo_id",
                        column: x => x.photo_id,
                        principalTable: "Photos",
                        principalColumn: "photo_id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "PhotoAlbums",
                columns: table => new
                {
                    album_id = table.Column<int>(type: "int", nullable: false),
                    photo_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoAlbums", x => new { x.photo_id, x.album_id });
                    table.ForeignKey(
                        name: "FK_PhotoAlbums_Albums_album_id",
                        column: x => x.album_id,
                        principalTable: "Albums",
                        principalColumn: "album_id");
                    table.ForeignKey(
                        name: "FK_PhotoAlbums_Photos_photo_id",
                        column: x => x.photo_id,
                        principalTable: "Photos",
                        principalColumn: "photo_id");
                });

            migrationBuilder.CreateTable(
                name: "PhotoCategories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false),
                    photo_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoCategories", x => new { x.photo_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_PhotoCategories_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK_PhotoCategories_Photos_photo_id",
                        column: x => x.photo_id,
                        principalTable: "Photos",
                        principalColumn: "photo_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_user_id",
                table: "Albums",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_photo_id",
                table: "Comments",
                column: "photo_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_user_id",
                table: "Comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoAlbums_album_id",
                table: "PhotoAlbums",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoCategories_category_id",
                table: "PhotoCategories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_user_id",
                table: "Photos",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PhotoAlbums");

            migrationBuilder.DropTable(
                name: "PhotoCategories");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "id");
        }
    }
}
