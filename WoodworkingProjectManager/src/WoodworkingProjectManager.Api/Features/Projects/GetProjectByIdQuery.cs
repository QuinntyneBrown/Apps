using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Projects;

public record GetProjectByIdQuery : IRequest<ProjectDto?>
{
    public Guid ProjectId { get; init; }
}

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;

    public GetProjectByIdQueryHandler(
        IWoodworkingProjectManagerContext context,
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

        if (project == null)
        {
            _logger.LogWarning("Project {ProjectId} not found", request.ProjectId);
            return null;
        }

        return project.ToDto();
    }
}
