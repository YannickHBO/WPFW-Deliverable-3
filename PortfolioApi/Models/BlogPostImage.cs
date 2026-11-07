namespace PortfolioApi.Models;

/// <summary>EF Core entity: one image belonging to a blog post.</summary>
public class BlogPostImage
{
    public int Id { get; set; }

    /// <summary>Foreign key to the blog post this image belongs to.</summary>
    public int BlogPostId { get; set; }
    public BlogPost? BlogPost { get; set; }

    /// <summary>Image location, e.g. "/images/blog/photo.jpg" (served by the frontend).</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>Alt text for screen readers (WCAG 1.1.1).</summary>
    public string Alt { get; set; } = string.Empty;

    /// <summary>Position in the post's gallery (0 = first).</summary>
    public int SortOrder { get; set; }
}
