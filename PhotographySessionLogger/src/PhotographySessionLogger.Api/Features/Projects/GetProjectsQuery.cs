using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Projects;

public record GetProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsCompleted { get; init; }
}

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetProjectsQueryHandler> _logger;

    public GetProjectsQueryHandler(
        IPhotographySessionLoggerContext context,
        ILogger<GetProjectsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting projects for user {UserId}", request.UserId);

        var query = _context.Projects.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == request.UserId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(p => p.IsCompleted == request.IsCompleted.Value);
        }

        var projects = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return projects.Select(p => p.ToDto());
    }
}
