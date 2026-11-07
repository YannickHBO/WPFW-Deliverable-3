using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Dtos;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers;

/// <summary>HTTP layer only: routing, status codes and validation results. Logic lives in IProjectService.</summary>
[ApiController]
[Route("api/projects")]
[Produces("application/json")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<ProjectDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProjectDto>>> GetAll() =>
        Ok(await projectService.GetAllAsync());

    [HttpGet("{id:int}")]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> GetById(int id)
    {
        var project = await projectService.GetByIdAsync(id);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpPost]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto dto)
    {
        var created = await projectService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<ProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> Update(int id, UpdateProjectDto dto)
    {
        var updated = await projectService.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) =>
        await projectService.DeleteAsync(id) ? NoContent() : NotFound();
}
