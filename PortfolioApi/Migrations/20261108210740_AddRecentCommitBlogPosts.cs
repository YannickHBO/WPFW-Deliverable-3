using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRecentCommitBlogPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Content", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 14, "Clean up README by removing outdated sections", new DateTime(2026, 11, 7, 14, 20, 19, 0, DateTimeKind.Unspecified), "Blog Post 14" },
                    { 15, "Add blog galleries, project tags and site content", new DateTime(2026, 11, 7, 15, 53, 29, 0, DateTimeKind.Unspecified), "Blog Post 15" },
                    { 16, "Revert \"Add blog galleries, project tags and site content\"", new DateTime(2026, 11, 7, 15, 55, 35, 0, DateTimeKind.Unspecified), "Blog Post 16" },
                    { 17, "Add blog galleries, project tags and site content", new DateTime(2026, 11, 7, 16, 8, 50, 0, DateTimeKind.Unspecified), "Blog Post 17" },
                    { 18, "Enhance README with setup and architecture details", new DateTime(2026, 11, 7, 16, 27, 9, 0, DateTimeKind.Unspecified), "Blog Post 18" },
                    { 19, "Updated readme to add frontend run commands", new DateTime(2026, 11, 8, 14, 21, 59, 0, DateTimeKind.Unspecified), "Blog Post 19" },
                    { 20, "Readme opgeschoond, technische documentatie naar architectuur document verplaatst.", new DateTime(2026, 11, 8, 19, 53, 43, 0, DateTimeKind.Unspecified), "Blog Post 20" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
