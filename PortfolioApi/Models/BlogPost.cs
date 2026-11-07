namespace PortfolioApi.Models;

/// <summary>EF Core entity. Never exposed directly by the API — see Dtos/BlogPostDtos.cs.</summary>
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateOnly PublishedDate { get; set; }
}
