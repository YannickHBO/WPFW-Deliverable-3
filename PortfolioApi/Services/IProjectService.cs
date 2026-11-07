using PortfolioApi.Dtos;

namespace PortfolioApi.Services;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(int id);
    Task<ProjectDto> CreateAsync(CreateProjectDto dto);
    /// <returns>The updated project, or null when no project with this id exists.</returns>
    Task<ProjectDto?> UpdateAsync(int id, UpdateProjectDto dto);
    /// <returns>False when no project with this id exists.</returns>
    Task<bool> DeleteAsync(int id);
}
