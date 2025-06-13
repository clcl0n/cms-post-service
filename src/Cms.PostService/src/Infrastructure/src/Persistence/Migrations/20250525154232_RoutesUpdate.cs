using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoutesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "route_ids",
                schema: "cms-post-service",
                table: "post");

            migrationBuilder.AddColumn<string>(
                name: "routes",
                schema: "cms-post-service",
                table: "post",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "routes",
                schema: "cms-post-service",
                table: "post");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "route_ids",
                schema: "cms-post-service",
                table: "post",
                type: "uuid[]",
                nullable: false);
        }
    }
}
