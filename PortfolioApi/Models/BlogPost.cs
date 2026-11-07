namespace PortfolioApi.Models;

/// <summary>EF Core entity. Never exposed directly by the API — see Dtos/BlogPostDtos.cs.</summary>
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }

    /// <summary>Optional photos shown with the post (one-to-many).</summary>
    public List<BlogPostImage> Images { get; set; } = [];
}
