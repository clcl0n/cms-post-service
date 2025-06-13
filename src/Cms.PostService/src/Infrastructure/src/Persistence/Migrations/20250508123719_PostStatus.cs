using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PostStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "cms-post-service",
                table: "post",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                schema: "cms-post-service",
                table: "post");
        }
    }
}
