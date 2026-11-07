using PortfolioApi.Dtos;
using PortfolioApi.Models;

namespace PortfolioApi.Mappings;

/// <summary>Converts between the Project entity and its DTOs.</summary>
public static class ProjectMappings
{
    public static ProjectDto ToDto(this Project project) =>
        new(project.Id, project.Title, project.Description, project.Category, project.GitHubUrl, project.Date);

    public static Project ToEntity(this CreateProjectDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        Category = dto.Category,
        GitHubUrl = dto.GitHubUrl,
        Date = dto.Date
    };

    public static void ApplyTo(this UpdateProjectDto dto, Project project)
    {
        project.Title = dto.Title;
        project.Description = dto.Description;
        project.Category = dto.Category;
        project.GitHubUrl = dto.GitHubUrl;
        project.Date = dto.Date;
    }
}
