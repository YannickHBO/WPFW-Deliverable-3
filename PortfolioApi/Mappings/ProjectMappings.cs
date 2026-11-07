using PortfolioApi.Dtos;
using PortfolioApi.Models;

namespace PortfolioApi.Mappings;

/// <summary>Converts between the Project entity and its DTOs.</summary>
public static class ProjectMappings
{
    public static ProjectDto ToDto(this Project project) =>
        new(project.Id, project.Title, project.Description, project.Category, project.GitHubUrl, project.Date,
            project.InProgress, project.Tags.ToList());

    public static Project ToEntity(this CreateProjectDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        Category = dto.Category,
        GitHubUrl = dto.GitHubUrl,
        Date = dto.Date,
        InProgress = dto.InProgress,
        Tags = CleanTags(dto.Tags)
    };

    public static void ApplyTo(this UpdateProjectDto dto, Project project)
    {
        project.Title = dto.Title;
        project.Description = dto.Description;
        project.Category = dto.Category;
        project.GitHubUrl = dto.GitHubUrl;
        project.Date = dto.Date;
        project.InProgress = dto.InProgress;
        project.Tags = CleanTags(dto.Tags);
    }

    private static List<string> CleanTags(IEnumerable<string>? tags) =>
        (tags ?? []).Select(tag => tag.Trim()).ToList();
}
