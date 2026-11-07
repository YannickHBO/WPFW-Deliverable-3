using PortfolioApi.Dtos;
using PortfolioApi.Models;

namespace PortfolioApi.Mappings;

/// <summary>Converts between the BlogPost entity and its DTOs.</summary>
public static class BlogPostMappings
{
    public static BlogPostDto ToDto(this BlogPost post) =>
        new(post.Id, post.Title, post.Content, post.PublishedDate);

    public static BlogPost ToEntity(this CreateBlogPostDto dto) => new()
    {
        Title = dto.Title,
        Content = dto.Content,
        PublishedDate = dto.PublishedDate
    };

    public static void ApplyTo(this UpdateBlogPostDto dto, BlogPost post)
    {
        post.Title = dto.Title;
        post.Content = dto.Content;
        post.PublishedDate = dto.PublishedDate;
    }
}
