using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Projects;

public record ToggleProjectFeaturedCommand : IRequest<ProjectDto?>
{
    public Guid ProjectId { get; init; }
}

public class ToggleProjectFeaturedCommandHandler : IRequestHandler<ToggleProjectFeaturedCommand, ProjectDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<ToggleProjectFeaturedCommandHandler> _logger;

    public ToggleProjectFeaturedCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<ToggleProjectFeaturedCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProjectDto?> Handle(ToggleProjectFeaturedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Toggling featured status for project {ProjectId}", request.ProjectId);

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project == null)
        {
            _logger.LogWarning("Project {ProjectId} not found", request.ProjectId);
            return null;
        }

        project.ToggleFeatured();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Toggled featured status for project {ProjectId} to {IsFeatured}",
            request.ProjectId,
            project.IsFeatured);

        return project.ToDto();
    }
}
