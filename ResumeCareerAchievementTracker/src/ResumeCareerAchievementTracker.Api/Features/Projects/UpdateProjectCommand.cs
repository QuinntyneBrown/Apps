using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Projects;

public record UpdateProjectCommand : IRequest<ProjectDto?>
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? Organization { get; init; }
    public string? Role { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<string>? Technologies { get; init; }
    public List<string>? Outcomes { get; init; }
    public string? ProjectUrl { get; init; }
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    public UpdateProjectCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<UpdateProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProjectDto?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating project {ProjectId}", request.ProjectId);

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project == null)
        {
            _logger.LogWarning("Project {ProjectId} not found", request.ProjectId);
            return null;
        }

        project.Name = request.Name;
        project.Description = request.Description;
        project.Organization = request.Organization;
        project.Role = request.Role;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.Technologies = request.Technologies ?? new List<string>();
        project.Outcomes = request.Outcomes ?? new List<string>();
        project.ProjectUrl = request.ProjectUrl;
        project.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated project {ProjectId}", request.ProjectId);

        return project.ToDto();
    }
}
