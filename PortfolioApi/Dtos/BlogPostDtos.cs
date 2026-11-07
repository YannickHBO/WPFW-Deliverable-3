using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Dtos;

/// <summary>What the API returns for a blog post.</summary>
public record BlogPostDto(
    int Id,
    string Title,
    string Content,
    DateOnly PublishedDate);

/// <summary>Request body for POST /api/blogposts.</summary>
public record CreateBlogPostDto(
    [Required, StringLength(150, MinimumLength = 1)] string Title,
    [Required, MinLength(1)] string Content,
    DateOnly PublishedDate);

/// <summary>Request body for PUT /api/blogposts/{id}.</summary>
public record UpdateBlogPostDto(
    [Required, StringLength(150, MinimumLength = 1)] string Title,
    [Required, MinLength(1)] string Content,
    DateOnly PublishedDate);
