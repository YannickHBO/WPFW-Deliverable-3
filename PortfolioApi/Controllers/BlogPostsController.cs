using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Dtos;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers;

/// <summary>HTTP layer only: routing, status codes and validation results. Logic lives in IBlogPostService.</summary>
[ApiController]
[Route("api/blogposts")]
[Produces("application/json")]
public class BlogPostsController(IBlogPostService blogPostService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<BlogPostDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BlogPostDto>>> GetAll() =>
        Ok(await blogPostService.GetAllAsync());

    [HttpGet("{id:int}")]
    [ProducesResponseType<BlogPostDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogPostDto>> GetById(int id)
    {
        var post = await blogPostService.GetByIdAsync(id);
        return post is null ? NotFound() : Ok(post);
    }

    [HttpPost]
    [ProducesResponseType<BlogPostDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlogPostDto>> Create(CreateBlogPostDto dto)
    {
        var created = await blogPostService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<BlogPostDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogPostDto>> Update(int id, UpdateBlogPostDto dto)
    {
        var updated = await blogPostService.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) =>
        await blogPostService.DeleteAsync(id) ? NoContent() : NotFound();
}
