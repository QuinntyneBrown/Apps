using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Projects;

public record GetProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{
    public Guid? UserId { get; init; }
    public bool? FeaturedOnly { get; init; }
    public bool? ActiveOnly { get; init; }
}

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<GetProjectsQueryHandler> _logger;

    public GetProjectsQueryHandler(
        IResumeCareerAchievementTrackerContext context,
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

        if (request.FeaturedOnly == true)
        {
            query = query.Where(p => p.IsFeatured);
        }

        if (request.ActiveOnly == true)
        {
            query = query.Where(p => p.EndDate == null);
        }

        var projects = await query
            .OrderByDescending(p => p.StartDate)
            .ToListAsync(cancellationToken);

        return projects.Select(p => p.ToDto());
    }
}
