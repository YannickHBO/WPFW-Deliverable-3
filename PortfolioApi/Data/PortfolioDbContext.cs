using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Data;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(project =>
        {
            project.Property(p => p.Title).HasMaxLength(100).IsRequired();
            project.Property(p => p.Description).HasMaxLength(2000).IsRequired();
            project.Property(p => p.Category).HasMaxLength(50).IsRequired();
            project.Property(p => p.GitHubUrl).HasMaxLength(300);

            project.HasData(
                new Project { Id = 1, Title = "Lorem Project", Category = "Web design", Date = new DateOnly(2026, 1, 15),
                    Description = "Here you will find a description of the site; Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
                new Project { Id = 2, Title = "Ipsum Project", Category = "Web development", Date = new DateOnly(2026, 2, 20),
                    Description = "Pellentesque a nisl lacus. Pellentesque scelerisque finibus interdum. Mauris consectetur lectus vitae urna vestibulum." },
                new Project { Id = 3, Title = "Dolor Project", Category = "Accessibility", Date = new DateOnly(2026, 3, 25),
                    Description = "Nunc sed nisl ac ex dignissim aliquam. Duis lorem magna, mattis non placerat ac, iaculis vestibulum velit." });
        });

        modelBuilder.Entity<BlogPost>(post =>
        {
            post.Property(b => b.Title).HasMaxLength(150).IsRequired();
            post.Property(b => b.Content).IsRequired();

            post.HasData(
                new BlogPost { Id = 1, Title = "Blog Post 1", PublishedDate = new DateOnly(2026, 1, 12),
                    Content = "Here you will find a description of the site; Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
                new BlogPost { Id = 2, Title = "Blog Post 2", PublishedDate = new DateOnly(2026, 2, 5),
                    Content = "Pellentesque a nisl lacus. Pellentesque scelerisque finibus interdum. Mauris consectetur lectus vitae urna vestibulum." });
        });
    }
}
