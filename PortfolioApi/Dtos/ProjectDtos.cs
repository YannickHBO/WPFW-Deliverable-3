using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Dtos;

/// <summary>What the API returns for a project.</summary>
public record ProjectDto(
    int Id,
    string Title,
    string Description,
    string Category,
    string? GitHubUrl,
    DateOnly Date);

/// <summary>Request body for POST /api/projects. No Id: the database generates it.</summary>
public record CreateProjectDto(
    [Required, StringLength(100, MinimumLength = 1)] string Title,
    [Required, StringLength(2000, MinimumLength = 1)] string Description,
    [Required, StringLength(50, MinimumLength = 1)] string Category,
    [Url, StringLength(300)] string? GitHubUrl,
    DateOnly Date);

/// <summary>Request body for PUT /api/projects/{id}. The Id comes from the route.</summary>
public record UpdateProjectDto(
    [Required, StringLength(100, MinimumLength = 1)] string Title,
    [Required, StringLength(2000, MinimumLength = 1)] string Description,
    [Required, StringLength(50, MinimumLength = 1)] string Category,
    [Url, StringLength(300)] string? GitHubUrl,
    DateOnly Date);
