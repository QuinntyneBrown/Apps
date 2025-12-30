using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Projects;

public record GetProjectByIdQuery : IRequest<ProjectDto?>
{
    public Guid ProjectId { get; init; }
}

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;

    public GetProjectByIdQueryHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<GetProjectByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting project {ProjectId}", request.ProjectId);

        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        return project?.ToDto();
    }
}
