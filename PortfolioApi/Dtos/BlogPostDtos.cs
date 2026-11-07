using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Dtos;

/// <summary>What the API returns for a blog post. Images are in gallery order.</summary>
public record BlogPostDto(
    int Id,
    string Title,
    string Content,
    DateTime PublishedDate,
    IReadOnlyList<BlogPostImageDto> Images);

/// <summary>Request body for POST /api/blogposts. Images are optional; their order is the gallery order.</summary>
public record CreateBlogPostDto(
    [Required, StringLength(150, MinimumLength = 1)] string Title,
    [Required, MinLength(1)] string Content,
    DateTime PublishedDate,
    [MaxLength(10)] List<BlogPostImageDto>? Images = null);

/// <summary>Request body for PUT /api/blogposts/{id}. The images sent here replace the existing ones.</summary>
public record UpdateBlogPostDto(
    [Required, StringLength(150, MinimumLength = 1)] string Title,
    [Required, MinLength(1)] string Content,
    DateTime PublishedDate,
    [MaxLength(10)] List<BlogPostImageDto>? Images = null);

/// <summary>An image in a blog post, used for both input and output. Alt text is required (WCAG 1.1.1).</summary>
public record BlogPostImageDto(
    [Required, StringLength(300, MinimumLength = 1)] string Url,
    [Required, StringLength(300, MinimumLength = 1)] string Alt);
