using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedPortfolioContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InProgress",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "BlogPosts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "BlogPostImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostImages_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BlogPostImages",
                columns: new[] { "Id", "Alt", "BlogPostId", "SortOrder", "Url" },
                values: new object[,]
                {
                    { 1, "Moss-covered stone torii gate in a green forest at the start of the Sengen trail, with stone guardian statues on both sides and stone steps leading up the path.", 1, 0, "/images/blog/fuji-2026-01-sengen-torii.jpg" },
                    { 2, "Yellow barrier boards across the trail with signs in Japanese and English: 'Do not entry! Winter closure: no entry beyond this point from 11 September.'", 1, 1, "/images/blog/fuji-2026-02-winter-closure.jpg" },
                    { 3, "Looking up a steep slope of black volcanic rock and red gravel towards the snow-covered top of Mount Fuji under a blue sky, with a red torii gate on the ridge.", 1, 2, "/images/blog/fuji-2026-03-snowy-ascent.jpg" },
                    { 4, "Garmin watch showing the elevation profile of the route, peaking at 3,703 m, with the part already climbed in green and the rest in blue, over 38.86 km.", 1, 3, "/images/blog/fuji-2026-04-elevation-profile.jpg" },
                    { 5, "A mountain hut walkway half buried in snow, with broken wooden beams and panels hanging over the railing in the fog.", 1, 4, "/images/blog/fuji-2026-05-damaged-hut.jpg" },
                    { 6, "A snow-covered path between dark volcanic rocks leading up to a wooden torii gate and a stone guardian lion near the summit, under a blue sky.", 1, 5, "/images/blog/fuji-2026-06-summit-torii.jpg" },
                    { 7, "View across the summit crater of Mount Fuji: steep black and rust-red crater walls with patches of snow, and weathered wooden posts in the red gravel in front.", 1, 6, "/images/blog/fuji-2026-07-crater.jpg" },
                    { 8, "The snow-covered summit crater with fog drifting through it, and an information sign about Mount Fuji as a volcano in the foreground.", 1, 7, "/images/blog/fuji-2026-08-crater-fog.jpg" },
                    { 9, "Garmin watch at the end of the hike showing 42.48 km, 3,043 m of ascent and a time of 15:00 hours.", 1, 8, "/images/blog/fuji-2026-09-watch-stats.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "PublishedDate" },
                values: new object[] { "Climbed Mount Fuji via the Sengen trail, before the official climbing season had started. The upper mountain was still covered in snow, which made the going slow: the whole journey took 15 hours, with 42 kilometres and roughly 3,000 metres of ascent and descent.", new DateTime(2026, 5, 24, 21, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "PublishedDate" },
                values: new object[] { "Add initial todo list to README", new DateTime(2026, 9, 14, 11, 28, 13, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Content", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 3, "v0.1", new DateTime(2026, 9, 14, 12, 53, 58, 0, DateTimeKind.Unspecified), "Blog Post 3" },
                    { 4, "v.2 changed formatting to allow github pages functionality", new DateTime(2026, 9, 14, 13, 5, 9, 0, DateTimeKind.Unspecified), "Blog Post 4" },
                    { 5, "v0.3 aria accessibility support, branding design updates, gradient more aggressive.", new DateTime(2026, 9, 14, 13, 50, 10, 0, DateTimeKind.Unspecified), "Blog Post 5" },
                    { 6, "v.4 added seemless scrolling functionality that loops through the pages with a small fade effect.", new DateTime(2026, 9, 14, 14, 3, 29, 0, DateTimeKind.Unspecified), "Blog Post 6" },
                    { 7, "v.5 scrolling feature cue is not quite working yet, will give it another look over later.", new DateTime(2026, 9, 14, 14, 45, 2, 0, DateTimeKind.Unspecified), "Blog Post 7" },
                    { 8, "scroll functionality has been rewritten, scrolling cue added based on arthurs advice. minimal page length removed to reduce unnecessary scrolling. Profile picture placeholder updated and adjusted to be more ipad userfriendly. Aria labels added to scrolling cues and project data moved from being hardcoded in html to project.json", new DateTime(2026, 9, 28, 13, 46, 9, 0, DateTimeKind.Unspecified), "Blog Post 8" },
                    { 9, "added search bar so users can filter through projects", new DateTime(2026, 9, 28, 13, 51, 7, 0, DateTimeKind.Unspecified), "Blog Post 9" },
                    { 10, "contact form added with a bunch of validation and user feedback. Weather api implemented and coupled to new dark mode feature. (weather code >3: darkmode on by default).", new DateTime(2026, 9, 28, 14, 51, 56, 0, DateTimeKind.Unspecified), "Blog Post 10" },
                    { 11, "Added extra elements to dark mode: logo with inverted colors, higher contrast labels. Also reduced the width of the hamburgermenu when expanded by replacing light and dark text with sun and moon icons, also removed redundant auto:mode text since it will be very visually apparent already what mode is currently running.", new DateTime(2026, 9, 28, 15, 17, 49, 0, DateTimeKind.Unspecified), "Blog Post 11" },
                    { 12, "No commit this time, but definitely a milestone worth logging: today I ran the very first edition of The Hague Marathon. 42.195 kilometres, finished in 3:35:59.", new DateTime(2026, 11, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), "Blog Post 12" },
                    { 13, "Portfolio API (.NET 10, EF Core, SQL Server) en React-frontend die projecten en blogposts uit de API laadt", new DateTime(2026, 11, 7, 14, 11, 50, 0, DateTimeKind.Unspecified), "Blog Post 13" }
                });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "Date", "Description", "InProgress", "Tags", "Title" },
                values: new object[] { "Web development", new DateOnly(2023, 6, 1), "A movie database I built for a family member to keep track of their film collection: browse, search and add movies in one place instead of scattered lists.", false, "[\"JavaScript\",\"HTML \\u0026 CSS\",\"SQL\"]", "Movie Database" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "Date", "Description", "InProgress", "Tags", "Title" },
                values: new object[] { "Accessibility", new DateOnly(2024, 12, 1), "A website built for Stichting Accessibility, with accessibility as the starting point: designed and tested against WCAG so it works for screen reader users, keyboard users and people with low vision.", false, "[\"WCAG\",\"HTML \\u0026 CSS\",\"JavaScript\"]", "Stichting Accessibility Website" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Date", "Description", "InProgress", "Tags", "Title" },
                values: new object[] { "Cybersecurity", new DateOnly(2026, 6, 1), "An investigation into package poisoning and the Shai-Hulud worms that have been hitting the developer community since late 2025. Shai-Hulud spreads through npm: an infected package runs code on install, steals developer credentials such as npm and GitHub tokens, and uses them to publish poisoned versions of other packages its victim maintains. I looked at how these attacks spread, how to detect them, and how to defend against them: lockfiles and pinned versions, disabling install scripts, waiting before adopting brand-new releases, and scoped, short-lived publishing tokens.", false, "[\"Supply chain\",\"npm\",\"Cybersecurity\"]", "Package Poisoning Investigation" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Category", "Date", "Description", "GitHubUrl", "InProgress", "Tags", "Title" },
                values: new object[,]
                {
                    { 4, "Cybersecurity", new DateOnly(2026, 9, 19), "A large language model running entirely on my own hardware instead of in the cloud. Prompts and documents never leave the machine, so there is no third party that can store, read or train on them, no API keys that can leak, and no data crossing the internet. It also works offline and keeps me in control of which model runs and when it gets updated, which makes it suitable for sensitive or confidential data.", null, false, "[\"Local LLM\",\"Cybersecurity\",\"Self-hosting\"]", "Local AI Assistant" },
                    { 5, "DevOps", new DateOnly(2026, 11, 1), "Currently working on setting up my own Kubernetes cluster: learning how to deploy containerised applications, scale them, and keep them running reliably and securely.", null, true, "[\"Kubernetes\",\"Docker\",\"DevOps\"]", "Kubernetes Cluster" },
                    { 6, "Web development", new DateOnly(2026, 11, 7), "This website: a React frontend with a typewriter look, weather-based dark mode and photo galleries, backed by my own ASP.NET Core Web API that stores projects and blog posts in SQL Server via EF Core.", "https://github.com/YannickHBO/WPFW-Deliverable-3", false, "[\"React\",\"ASP.NET Core\",\"SQL Server\"]", "WPFWDesigns Portfolio" }
                });

            migrationBuilder.InsertData(
                table: "BlogPostImages",
                columns: new[] { "Id", "Alt", "BlogPostId", "SortOrder", "Url" },
                values: new object[,]
                {
                    { 10, "Black-and-white photo of me running The Hague Marathon with bib number 9396, smiling and waving at the camera.", 12, 0, "/images/blog/the-hague-marathon-2026.jpg" },
                    { 11, "Strava screenshot of The Hague Marathon on November 1, 2026: a route map through The Hague and along the coast with personal-record badges, and the stats 42.44 km, average pace 5:05 per km, moving time 3:36:05, 59 m elevation gain, 3,012 calories and average heart rate 159 bpm.", 12, 1, "/images/blog/the-hague-marathon-2026-strava.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostImages_BlogPostId",
                table: "BlogPostImages",
                column: "BlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostImages");

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "InProgress",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Projects");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PublishedDate",
                table: "BlogPosts",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "PublishedDate" },
                values: new object[] { "Here you will find a description of the site; Lorem ipsum dolor sit amet, consectetur adipiscing elit.", new DateOnly(2026, 1, 12) });

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "PublishedDate" },
                values: new object[] { "Pellentesque a nisl lacus. Pellentesque scelerisque finibus interdum. Mauris consectetur lectus vitae urna vestibulum.", new DateOnly(2026, 2, 5) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "Date", "Description", "Title" },
                values: new object[] { "Web design", new DateOnly(2026, 1, 15), "Here you will find a description of the site; Lorem ipsum dolor sit amet, consectetur adipiscing elit.", "Lorem Project" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "Date", "Description", "Title" },
                values: new object[] { "Web development", new DateOnly(2026, 2, 20), "Pellentesque a nisl lacus. Pellentesque scelerisque finibus interdum. Mauris consectetur lectus vitae urna vestibulum.", "Ipsum Project" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Date", "Description", "Title" },
                values: new object[] { "Accessibility", new DateOnly(2026, 3, 25), "Nunc sed nisl ac ex dignissim aliquam. Duis lorem magna, mattis non placerat ac, iaculis vestibulum velit.", "Dolor Project" });
        }
    }
}
