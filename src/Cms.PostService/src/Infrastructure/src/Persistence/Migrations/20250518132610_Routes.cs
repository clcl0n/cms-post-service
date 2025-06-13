using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Routes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "body_plan_text",
                schema: "cms-post-service",
                table: "post",
                newName: "slug");

            migrationBuilder.AddColumn<string>(
                name: "body_plain_text",
                schema: "cms-post-service",
                table: "post",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "route_ids",
                schema: "cms-post-service",
                table: "post",
                type: "uuid[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "body_plain_text",
                schema: "cms-post-service",
                table: "post");

            migrationBuilder.DropColumn(
                name: "route_ids",
                schema: "cms-post-service",
                table: "post");

            migrationBuilder.RenameColumn(
                name: "slug",
                schema: "cms-post-service",
                table: "post",
                newName: "body_plan_text");
        }
    }
}
