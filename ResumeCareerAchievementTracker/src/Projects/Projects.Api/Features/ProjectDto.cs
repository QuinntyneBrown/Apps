using Projects.Core.Models;

namespace Projects.Api.Features;

public record ProjectDto(
    Guid ProjectId,
    Guid UserId,
    string Name,
    string Description,
    string? Organization,
    string? Role,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsFeatured,
    DateTime CreatedAt);

public static class ProjectExtensions
{
    public static ProjectDto ToDto(this Project project)
    {
        return new ProjectDto(
            project.ProjectId,
            project.UserId,
            project.Name,
            project.Description,
            project.Organization,
            project.Role,
            project.StartDate,
            project.EndDate,
            project.IsFeatured,
            project.CreatedAt);
    }
}
