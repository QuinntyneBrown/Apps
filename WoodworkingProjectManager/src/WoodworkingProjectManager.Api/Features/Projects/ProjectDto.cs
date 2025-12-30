using WoodworkingProjectManager.Core;

namespace WoodworkingProjectManager.Api.Features.Projects;

public record ProjectDto
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ProjectStatus Status { get; init; }
    public WoodType WoodType { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? CompletionDate { get; init; }
    public decimal? EstimatedCost { get; init; }
    public decimal? ActualCost { get; init; }
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
            Status = project.Status,
            WoodType = project.WoodType,
            StartDate = project.StartDate,
            CompletionDate = project.CompletionDate,
            EstimatedCost = project.EstimatedCost,
            ActualCost = project.ActualCost,
            Notes = project.Notes,
            CreatedAt = project.CreatedAt,
        };
    }
}
