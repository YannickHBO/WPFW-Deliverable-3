using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Data;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<BlogPostImage> BlogPostImages => Set<BlogPostImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(project =>
        {
            project.Property(p => p.Title).HasMaxLength(100).IsRequired();
            project.Property(p => p.Description).HasMaxLength(2000).IsRequired();
            project.Property(p => p.Category).HasMaxLength(50).IsRequired();
            project.Property(p => p.GitHubUrl).HasMaxLength(300);

            // Numbered in chronological order. Dates are month precision (day 1) unless known exactly;
            // the frontend shows projects as "month year".
            project.HasData(
                new Project { Id = 1, Title = "Movie Database", Category = "Web development", Date = new DateOnly(2023, 6, 1),
                    Description = "A movie database I built for a family member to keep track of their film collection: browse, search and add movies in one place instead of scattered lists.",
                    Tags = ["JavaScript", "HTML & CSS", "SQL"] },
                new Project { Id = 2, Title = "Stichting Accessibility Website", Category = "Accessibility", Date = new DateOnly(2024, 12, 1),
                    Description = "A website built for Stichting Accessibility, with accessibility as the starting point: designed and tested against WCAG so it works for screen reader users, keyboard users and people with low vision.",
                    Tags = ["WCAG", "HTML & CSS", "JavaScript"] },
                new Project { Id = 3, Title = "Package Poisoning Deepdive", Category = "Cybersecurity", Date = new DateOnly(2026, 6, 1),
                    Description = "An investigation into package poisoning and the Shai-Hulud worms that have been hitting the developer community since late 2025. Shai-Hulud spreads through npm: an infected package runs code on install, steals developer credentials such as npm and GitHub tokens, and uses them to publish poisoned versions of other packages its victim maintains. I looked at how these attacks spread, how to detect them, and how to defend against them: lockfiles and pinned versions, disabling install scripts, waiting before adopting brand-new releases, and scoped, short-lived publishing tokens.",
                    Tags = ["Supply chain", "npm", "Cybersecurity"] },
                new Project { Id = 4, Title = "Local AI Assistant", Category = "Cybersecurity", Date = new DateOnly(2026, 9, 19),
                    Description = "A large language model running entirely on my own hardware instead of in the cloud. Prompts and documents never leave the machine, so there is no third party that can store, read or train on them, no API keys that can leak, and no data crossing the internet. It also works offline and keeps me in control of which model runs and when it gets updated, which makes it suitable for sensitive or confidential data.",
                    Tags = ["Local LLM", "Cybersecurity", "Self-hosting"] },
                new Project { Id = 5, Title = "Kubernetes Cluster", Category = "DevOps", Date = new DateOnly(2026, 11, 1),
                    InProgress = true,
                    Description = "Currently working on setting up my own Kubernetes cluster: learning how to deploy containerised applications, scale them, and keep them running reliably and securely.",
                    Tags = ["Kubernetes", "Docker", "DevOps"] },
                new Project { Id = 6, Title = "WPFWDesigns Portfolio", Category = "Web development", Date = new DateOnly(2026, 9, 7), InProgress = true,
                    Description = "This website: a React frontend with a typewriter look, weather-based dark mode and photo galleries, backed by my own ASP.NET Core Web API that stores projects and blog posts in SQL Server via EF Core.",
                    GitHubUrl = "https://github.com/YannickHBO/WPFW-Deliverable-3",
                    Tags = ["React", "ASP.NET Core", "SQL Server"] });
        });

        modelBuilder.Entity<BlogPost>(post =>
        {
            post.Property(b => b.Title).HasMaxLength(150).IsRequired();
            post.Property(b => b.Content).IsRequired();

            // One post has many images; deleting a post deletes its images.
            post.HasMany(b => b.Images)
                .WithOne(i => i.BlogPost)
                .HasForeignKey(i => i.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed blog posts: personal milestones plus the git commit history of this project
            // (commit message + commit date/time). Numbered in chronological order.
            post.HasData(
                // Not a commit: a personal milestone.
                new BlogPost { Id = 1, Title = "Blog Post 1", PublishedDate = new DateTime(2026, 5, 24, 21, 0, 0),
                    Content = "Climbed Mount Fuji via the Sengen trail, before the official climbing season had started. The upper mountain was still covered in snow, which made the going slow: the whole journey took 15 hours, with 42 kilometres and roughly 3,000 metres of ascent and descent." },
                new BlogPost { Id = 2, Title = "Blog Post 2", PublishedDate = new DateTime(2026, 9, 14, 11, 28, 13),
                    Content = "Add initial todo list to README" },
                new BlogPost { Id = 3, Title = "Blog Post 3", PublishedDate = new DateTime(2026, 9, 14, 12, 53, 58),
                    Content = "v0.1" },
                new BlogPost { Id = 4, Title = "Blog Post 4", PublishedDate = new DateTime(2026, 9, 14, 13, 5, 9),
                    Content = "v.2 changed formatting to allow github pages functionality" },
                new BlogPost { Id = 5, Title = "Blog Post 5", PublishedDate = new DateTime(2026, 9, 14, 13, 50, 10),
                    Content = "v0.3 aria accessibility support, branding design updates, gradient more aggressive." },
                new BlogPost { Id = 6, Title = "Blog Post 6", PublishedDate = new DateTime(2026, 9, 14, 14, 3, 29),
                    Content = "v.4 added seemless scrolling functionality that loops through the pages with a small fade effect." },
                new BlogPost { Id = 7, Title = "Blog Post 7", PublishedDate = new DateTime(2026, 9, 14, 14, 45, 2),
                    Content = "v.5 scrolling feature cue is not quite working yet, will give it another look over later." },
                new BlogPost { Id = 8, Title = "Blog Post 8", PublishedDate = new DateTime(2026, 9, 28, 13, 46, 9),
                    Content = "scroll functionality has been rewritten, scrolling cue added based on arthurs advice. minimal page length removed to reduce unnecessary scrolling. Profile picture placeholder updated and adjusted to be more ipad userfriendly. Aria labels added to scrolling cues and project data moved from being hardcoded in html to project.json" },
                new BlogPost { Id = 9, Title = "Blog Post 9", PublishedDate = new DateTime(2026, 9, 28, 13, 51, 7),
                    Content = "added search bar so users can filter through projects" },
                new BlogPost { Id = 10, Title = "Blog Post 10", PublishedDate = new DateTime(2026, 9, 28, 14, 51, 56),
                    Content = "contact form added with a bunch of validation and user feedback. Weather api implemented and coupled to new dark mode feature. (weather code >3: darkmode on by default)." },
                new BlogPost { Id = 11, Title = "Blog Post 11", PublishedDate = new DateTime(2026, 9, 28, 15, 17, 49),
                    Content = "Added extra elements to dark mode: logo with inverted colors, higher contrast labels. Also reduced the width of the hamburgermenu when expanded by replacing light and dark text with sun and moon icons, also removed redundant auto:mode text since it will be very visually apparent already what mode is currently running." },
                // Not a commit: a personal milestone.
                new BlogPost { Id = 12, Title = "Blog Post 12", PublishedDate = new DateTime(2026, 11, 1, 18, 0, 0),
                    Content = "No commit this time, but definitely a milestone worth logging: today I ran the very first edition of The Hague Marathon. 42.195 kilometres, finished in 3:35:59." },
                new BlogPost { Id = 13, Title = "Blog Post 13", PublishedDate = new DateTime(2026, 11, 7, 14, 11, 50),
                    Content = "Portfolio API (.NET 10, EF Core, SQL Server) en React-frontend die projecten en blogposts uit de API laadt" },
                new BlogPost { Id = 14, Title = "Blog Post 14", PublishedDate = new DateTime(2026, 11, 7, 14, 20, 19),
                    Content = "Clean up README by removing outdated sections" },
                new BlogPost { Id = 15, Title = "Blog Post 15", PublishedDate = new DateTime(2026, 11, 7, 15, 53, 29),
                    Content = "Add blog galleries, project tags and site content" },
                new BlogPost { Id = 16, Title = "Blog Post 16", PublishedDate = new DateTime(2026, 11, 7, 15, 55, 35),
                    Content = "Revert \"Add blog galleries, project tags and site content\"" },
                new BlogPost { Id = 17, Title = "Blog Post 17", PublishedDate = new DateTime(2026, 11, 7, 16, 8, 50),
                    Content = "Add blog galleries, project tags and site content" },
                new BlogPost { Id = 18, Title = "Blog Post 18", PublishedDate = new DateTime(2026, 11, 7, 16, 27, 9),
                    Content = "Enhance README with setup and architecture details" },
                new BlogPost { Id = 19, Title = "Blog Post 19", PublishedDate = new DateTime(2026, 11, 8, 14, 21, 59),
                    Content = "Updated readme to add frontend run commands" },
                new BlogPost { Id = 20, Title = "Blog Post 20", PublishedDate = new DateTime(2026, 11, 8, 19, 53, 43),
                    Content = "Readme opgeschoond, technische documentatie naar architectuur document verplaatst." });
        });

        modelBuilder.Entity<BlogPostImage>(image =>
        {
            image.Property(i => i.Url).HasMaxLength(300).IsRequired();
            image.Property(i => i.Alt).HasMaxLength(300).IsRequired();

            // Image files live in client/public/images/blog/. SortOrder = position in the post's gallery.
            image.HasData(
                // Blog Post 1: Mount Fuji climb.
                new BlogPostImage { Id = 1, BlogPostId = 1, SortOrder = 0,
                    Url = "/images/blog/fuji-2026-01-sengen-torii.jpg",
                    Alt = "Moss-covered stone torii gate in a green forest at the start of the Sengen trail, with stone guardian statues on both sides and stone steps leading up the path." },
                new BlogPostImage { Id = 2, BlogPostId = 1, SortOrder = 1,
                    Url = "/images/blog/fuji-2026-02-winter-closure.jpg",
                    Alt = "Yellow barrier boards across the trail with signs in Japanese and English: 'Do not entry! Winter closure: no entry beyond this point from 11 September.'" },
                new BlogPostImage { Id = 3, BlogPostId = 1, SortOrder = 2,
                    Url = "/images/blog/fuji-2026-03-snowy-ascent.jpg",
                    Alt = "Looking up a steep slope of black volcanic rock and red gravel towards the snow-covered top of Mount Fuji under a blue sky, with a red torii gate on the ridge." },
                new BlogPostImage { Id = 4, BlogPostId = 1, SortOrder = 3,
                    Url = "/images/blog/fuji-2026-04-elevation-profile.jpg",
                    Alt = "Garmin watch showing the elevation profile of the route, peaking at 3,703 m, with the part already climbed in green and the rest in blue, over 38.86 km." },
                new BlogPostImage { Id = 5, BlogPostId = 1, SortOrder = 4,
                    Url = "/images/blog/fuji-2026-05-damaged-hut.jpg",
                    Alt = "A mountain hut walkway half buried in snow, with broken wooden beams and panels hanging over the railing in the fog." },
                new BlogPostImage { Id = 6, BlogPostId = 1, SortOrder = 5,
                    Url = "/images/blog/fuji-2026-06-summit-torii.jpg",
                    Alt = "A snow-covered path between dark volcanic rocks leading up to a wooden torii gate and a stone guardian lion near the summit, under a blue sky." },
                new BlogPostImage { Id = 7, BlogPostId = 1, SortOrder = 6,
                    Url = "/images/blog/fuji-2026-07-crater.jpg",
                    Alt = "View across the summit crater of Mount Fuji: steep black and rust-red crater walls with patches of snow, and weathered wooden posts in the red gravel in front." },
                new BlogPostImage { Id = 8, BlogPostId = 1, SortOrder = 7,
                    Url = "/images/blog/fuji-2026-08-crater-fog.jpg",
                    Alt = "The snow-covered summit crater with fog drifting through it, and an information sign about Mount Fuji as a volcano in the foreground." },
                new BlogPostImage { Id = 9, BlogPostId = 1, SortOrder = 8,
                    Url = "/images/blog/fuji-2026-09-watch-stats.jpg",
                    Alt = "Garmin watch at the end of the hike showing 42.48 km, 3,043 m of ascent and a time of 15:00 hours." },

                // Blog Post 12: The Hague Marathon.
                new BlogPostImage { Id = 10, BlogPostId = 12, SortOrder = 0,
                    Url = "/images/blog/the-hague-marathon-2026.jpg",
                    Alt = "Black-and-white photo of me running The Hague Marathon with bib number 9396, smiling and waving at the camera." },
                new BlogPostImage { Id = 11, BlogPostId = 12, SortOrder = 1,
                    Url = "/images/blog/the-hague-marathon-2026-strava.jpg",
                    Alt = "Strava screenshot of The Hague Marathon on November 1, 2026: a route map through The Hague and along the coast with personal-record badges, and the stats 42.44 km, average pace 5:05 per km, moving time 3:36:05, 59 m elevation gain, 3,012 calories and average heart rate 159 bpm." });
        });
    }
}
