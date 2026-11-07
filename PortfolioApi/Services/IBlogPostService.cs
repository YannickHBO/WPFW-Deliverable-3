using PortfolioApi.Dtos;

namespace PortfolioApi.Services;

public interface IBlogPostService
{
    Task<IReadOnlyList<BlogPostDto>> GetAllAsync();
    Task<BlogPostDto?> GetByIdAsync(int id);
    Task<BlogPostDto> CreateAsync(CreateBlogPostDto dto);
    /// <returns>The updated blog post, or null when no blog post with this id exists.</returns>
    Task<BlogPostDto?> UpdateAsync(int id, UpdateBlogPostDto dto);
    /// <returns>False when no blog post with this id exists.</returns>
    Task<bool> DeleteAsync(int id);
}
