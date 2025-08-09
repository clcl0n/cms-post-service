using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.PostService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cms-post-service");

            migrationBuilder.CreateTable(
                name: "topic",
                schema: "cms-post-service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    routes = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic___id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post",
                schema: "cms-post-service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    body_plain_text = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    listing_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    topic_id = table.Column<Guid>(type: "uuid", nullable: false),
                    routes = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post___id", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_topic_topic_id",
                        column: x => x.topic_id,
                        principalSchema: "cms-post-service",
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subtopic",
                schema: "cms-post-service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_topic_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    routes = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subtopic___id", x => x.id);
                    table.ForeignKey(
                        name: "fk_subtopic_topic_parent_topic_id",
                        column: x => x.parent_topic_id,
                        principalSchema: "cms-post-service",
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "body_block",
                schema: "cms-post-service",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_body_block", x => new { x.post_id, x.order });
                    table.ForeignKey(
                        name: "FK_bb_post___post_id",
                        column: x => x.post_id,
                        principalSchema: "cms-post-service",
                        principalTable: "post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "paragraph_body_block",
                schema: "cms-post-service",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paragraph_body_block", x => new { x.post_id, x.order });
                    table.ForeignKey(
                        name: "FK_bb_paragraph_bb___post_id_order",
                        columns: x => new { x.post_id, x.order },
                        principalSchema: "cms-post-service",
                        principalTable: "body_block",
                        principalColumns: new[] { "post_id", "order" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bb___post_id",
                schema: "cms-post-service",
                table: "body_block",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_p_bb___post_id",
                schema: "cms-post-service",
                table: "paragraph_body_block",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_post___listing_image_id",
                schema: "cms-post-service",
                table: "post",
                column: "listing_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_post___topic_id",
                schema: "cms-post-service",
                table: "post",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "ix_subtopic_parent_topic_id",
                schema: "cms-post-service",
                table: "subtopic",
                column: "parent_topic_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paragraph_body_block",
                schema: "cms-post-service");

            migrationBuilder.DropTable(
                name: "subtopic",
                schema: "cms-post-service");

            migrationBuilder.DropTable(
                name: "body_block",
                schema: "cms-post-service");

            migrationBuilder.DropTable(
                name: "post",
                schema: "cms-post-service");

            migrationBuilder.DropTable(
                name: "topic",
                schema: "cms-post-service");
        }
    }
}
