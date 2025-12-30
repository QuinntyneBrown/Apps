using ResumeCareerAchievementTracker.Core;

namespace ResumeCareerAchievementTracker.Api.Features.Projects;

public record ProjectDto
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? Organization { get; init; }
    public string? Role { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<string> Technologies { get; init; } = new();
    public List<string> Outcomes { get; init; } = new();
    public string? ProjectUrl { get; init; }
    public bool IsFeatured { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ProjectExtensions
{
    public static ProjectDto ToDto(this Project project)
    {
        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            UserId = project.UserId,
            Name = project.Name,
            Description = project.Description,
            Organization = project.Organization,
            Role = project.Role,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Technologies = project.Technologies,
            Outcomes = project.Outcomes,
            ProjectUrl = project.ProjectUrl,
            IsFeatured = project.IsFeatured,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,
        };
    }
}
