using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Dtos;
using PortfolioApi.Mappings;

namespace PortfolioApi.Services;

public class ProjectService(PortfolioDbContext db) : IProjectService
{
    public async Task<IReadOnlyList<ProjectDto>> GetAllAsync()
    {
        var projects = await db.Projects
            .AsNoTracking()
            // Projects I'm still working on first, then the rest from newest to oldest.
            .OrderByDescending(p => p.InProgress)
            .ThenByDescending(p => p.Date)
            .ToListAsync();

        return projects.Select(p => p.ToDto()).ToList();
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await db.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return project?.ToDto();
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
    {
        var project = dto.ToEntity();
        db.Projects.Add(project);
        await db.SaveChangesAsync();
        return project.ToDto();
    }

    public async Task<ProjectDto?> UpdateAsync(int id, UpdateProjectDto dto)
    {
        var project = await db.Projects.FindAsync(id);
        if (project is null) return null;

        dto.ApplyTo(project);
        await db.SaveChangesAsync();
        return project.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await db.Projects.FindAsync(id);
        if (project is null) return false;

        db.Projects.Remove(project);
        await db.SaveChangesAsync();
        return true;
    }
}
