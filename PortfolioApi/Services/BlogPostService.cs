using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Dtos;
using PortfolioApi.Mappings;

namespace PortfolioApi.Services;

public class BlogPostService(PortfolioDbContext db) : IBlogPostService
{
    public async Task<IReadOnlyList<BlogPostDto>> GetAllAsync()
    {
        var posts = await db.BlogPosts
            .AsNoTracking()
            .Include(b => b.Images)
            .OrderByDescending(b => b.PublishedDate)
            .ToListAsync();

        return posts.Select(b => b.ToDto()).ToList();
    }

    public async Task<BlogPostDto?> GetByIdAsync(int id)
    {
        var post = await db.BlogPosts
            .AsNoTracking()
            .Include(b => b.Images)
            .FirstOrDefaultAsync(b => b.Id == id);
        return post?.ToDto();
    }

    public async Task<BlogPostDto> CreateAsync(CreateBlogPostDto dto)
    {
        var post = dto.ToEntity();
        db.BlogPosts.Add(post);
        await db.SaveChangesAsync();
        return post.ToDto();
    }

    public async Task<BlogPostDto?> UpdateAsync(int id, UpdateBlogPostDto dto)
    {
        // Load the existing images too: PUT replaces them with the ones in the request.
        var post = await db.BlogPosts
            .Include(b => b.Images)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (post is null) return null;

        dto.ApplyTo(post);
        await db.SaveChangesAsync();
        return post.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await db.BlogPosts.FindAsync(id);
        if (post is null) return false;

        db.BlogPosts.Remove(post);
        await db.SaveChangesAsync();
        return true;
    }
}
