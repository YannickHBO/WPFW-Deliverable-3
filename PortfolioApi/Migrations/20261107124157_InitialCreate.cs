using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioApi.Migrations
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
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GitHubUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Content", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, "Here you will find a description of the site; Lorem ipsum dolor sit amet, consectetur adipiscing elit.", new DateOnly(2026, 1, 12), "Blog Post 1" },
                    { 2, "Pellentesque a nisl lacus. Pellentesque scelerisque finibus interdum. Mauris consectetur lectus vitae urna vestibulum.", new DateOnly(2026, 2, 5), "Blog Post 2" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Category", "Date", "Description", "GitHubUrl", "Title" },
                values: new object[,]
                {
                    { 1, "Web design", new DateOnly(2026, 1, 15), "Here you will find a description of the site; Lorem ipsum dolor sit amet, consectetur adipiscing elit.", null, "Lorem Project" },
                    { 2, "Web development", new DateOnly(2026, 2, 20), "Pellentesque a nisl lacus. Pellentesque scelerisque finibus interdum. Mauris consectetur lectus vitae urna vestibulum.", null, "Ipsum Project" },
                    { 3, "Accessibility", new DateOnly(2026, 3, 25), "Nunc sed nisl ac ex dignissim aliquam. Duis lorem magna, mattis non placerat ac, iaculis vestibulum velit.", null, "Dolor Project" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
