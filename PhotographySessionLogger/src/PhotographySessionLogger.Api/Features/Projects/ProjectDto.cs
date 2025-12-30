using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Api.Features.Projects;

public record ProjectDto
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
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
            DueDate = project.DueDate,
            IsCompleted = project.IsCompleted,
            Notes = project.Notes,
            CreatedAt = project.CreatedAt,
        };
    }
}
