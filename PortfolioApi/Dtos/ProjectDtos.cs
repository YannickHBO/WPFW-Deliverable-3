using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Dtos;

/// <summary>
/// What the API returns for a project. A project is open source only when it has a public GitHubUrl;
/// without one it is treated as closed source.
/// </summary>
public record ProjectDto(
    int Id,
    string Title,
    string Description,
    string Category,
    string? GitHubUrl,
    DateOnly Date,
    bool InProgress,
    IReadOnlyList<string> Tags);

/// <summary>Request body for POST /api/projects. No Id: the database generates it.</summary>
public record CreateProjectDto(
    [Required, StringLength(100, MinimumLength = 1)] string Title,
    [Required, StringLength(2000, MinimumLength = 1)] string Description,
    [Required, StringLength(50, MinimumLength = 1)] string Category,
    [Url, StringLength(300)] string? GitHubUrl,
    DateOnly Date,
    List<string>? Tags = null,
    bool InProgress = false) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
        ProjectTagRules.Validate(Tags);
}

/// <summary>Request body for PUT /api/projects/{id}. The Id comes from the route; Tags replace the existing ones.</summary>
public record UpdateProjectDto(
    [Required, StringLength(100, MinimumLength = 1)] string Title,
    [Required, StringLength(2000, MinimumLength = 1)] string Description,
    [Required, StringLength(50, MinimumLength = 1)] string Category,
    [Url, StringLength(300)] string? GitHubUrl,
    DateOnly Date,
    List<string>? Tags = null,
    bool InProgress = false) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
        ProjectTagRules.Validate(Tags);
}

/// <summary>Business rules for project tags: at most 3, each 1–30 characters, no duplicates.</summary>
public static class ProjectTagRules
{
    public const int MaxTags = 3;
    public const int MaxTagLength = 30;

    public static IEnumerable<ValidationResult> Validate(List<string>? tags)
    {
        if (tags is null) yield break;

        string[] member = [nameof(CreateProjectDto.Tags)];

        if (tags.Count > MaxTags)
            yield return new ValidationResult($"A project can have at most {MaxTags} tags.", member);

        if (tags.Any(tag => string.IsNullOrWhiteSpace(tag) || tag.Trim().Length > MaxTagLength))
            yield return new ValidationResult($"Each tag must be 1 to {MaxTagLength} characters.", member);

        if (tags.Select(tag => tag?.Trim().ToLowerInvariant()).Distinct().Count() != tags.Count)
            yield return new ValidationResult("Tags must be unique.", member);
    }
}
