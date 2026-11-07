namespace PortfolioApi.Models;

/// <summary>EF Core entity. Never exposed directly by the API — see Dtos/ProjectDtos.cs.</summary>
public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? GitHubUrl { get; set; }
    public DateOnly Date { get; set; }

    /// <summary>True while I'm still working on the project.</summary>
    public bool InProgress { get; set; }

    /// <summary>Skills or programming languages used (max 3). Stored by EF Core as a JSON array column.</summary>
    public List<string> Tags { get; set; } = [];
}
