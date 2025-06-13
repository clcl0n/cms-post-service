using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "ix_post_topic_id",
                schema: "cms-post-service",
                table: "post",
                newName: "IX_post___topic_id");

            migrationBuilder.CreateIndex(
                name: "IX_post___listing_image_id",
                schema: "cms-post-service",
                table: "post",
                column: "listing_image_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_post___listing_image_id",
                schema: "cms-post-service",
                table: "post");

            migrationBuilder.RenameIndex(
                name: "IX_post___topic_id",
                schema: "cms-post-service",
                table: "post",
                newName: "ix_post_topic_id");
        }
    }
}
