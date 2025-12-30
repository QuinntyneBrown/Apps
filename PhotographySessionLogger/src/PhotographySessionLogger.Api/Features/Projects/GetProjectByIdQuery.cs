using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Projects;

public record GetProjectByIdQuery : IRequest<ProjectDto?>
{
    public Guid ProjectId { get; init; }
}

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;

    public GetProjectByIdQueryHandler(
        IPhotographySessionLoggerContext context,
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
