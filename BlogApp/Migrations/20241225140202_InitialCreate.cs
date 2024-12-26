using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Author", "Content", "CreatedDate", "HeaderImage", "Summary", "Tags", "Title" },
                values: new object[,]
                {
                    { 1, "Admin", "I just realized I haven't written about the AHA Stack on the blog...", new DateTime(2024, 12, 20, 17, 2, 2, 355, DateTimeKind.Local).AddTicks(3987), "https://astro.build/assets/press/logomark-light.svg", "Understanding the AHA Stack - Astro, HTMX, and Alpine.js", "[\"astro\",\"htmx\",\"alpine\",\"javascript\",\"web-development\"]", "The AHA Stack" },
                    { 2, "Admin", "The useOptimistic hook is a powerful feature in React...", new DateTime(2024, 12, 21, 17, 2, 2, 357, DateTimeKind.Local).AddTicks(525), "https://reactjs.org/logo-og.png", "Learn about optimistic updates in React", "[\"react\",\"hooks\",\"javascript\",\"optimization\"]", "The useOptimistic hook" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts");
        }
    }
}
