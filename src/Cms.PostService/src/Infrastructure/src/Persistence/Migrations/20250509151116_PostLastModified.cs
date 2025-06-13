using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PostLastModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified",
                schema: "cms-post-service",
                table: "post",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_modified",
                schema: "cms-post-service",
                table: "post");
        }
    }
}
