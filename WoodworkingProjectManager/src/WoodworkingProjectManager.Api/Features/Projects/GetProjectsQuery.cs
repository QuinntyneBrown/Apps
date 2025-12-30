using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Projects;

public record GetProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{
    public Guid? UserId { get; init; }
    public ProjectStatus? Status { get; init; }
    public WoodType? WoodType { get; init; }
}

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<GetProjectsQueryHandler> _logger;

    public GetProjectsQueryHandler(
        IWoodworkingProjectManagerContext context,
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

        if (request.Status.HasValue)
        {
            query = query.Where(p => p.Status == request.Status.Value);
        }

        if (request.WoodType.HasValue)
        {
            query = query.Where(p => p.WoodType == request.WoodType.Value);
        }

        var projects = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return projects.Select(p => p.ToDto());
    }
}
