using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Achievements;

public record GetAchievementsQuery : IRequest<IEnumerable<AchievementDto>>
{
    public Guid? UserId { get; init; }
    public AchievementType? AchievementType { get; init; }
    public bool? FeaturedOnly { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetAchievementsQueryHandler : IRequestHandler<GetAchievementsQuery, IEnumerable<AchievementDto>>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<GetAchievementsQueryHandler> _logger;

    public GetAchievementsQueryHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<GetAchievementsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<AchievementDto>> Handle(GetAchievementsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting achievements for user {UserId}", request.UserId);

        var query = _context.Achievements.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        if (request.AchievementType.HasValue)
        {
            query = query.Where(a => a.AchievementType == request.AchievementType.Value);
        }

        if (request.FeaturedOnly == true)
        {
            query = query.Where(a => a.IsFeatured);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(a => a.AchievedDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(a => a.AchievedDate <= request.EndDate.Value);
        }

        var achievements = await query
            .OrderByDescending(a => a.AchievedDate)
            .ToListAsync(cancellationToken);

        return achievements.Select(a => a.ToDto());
    }
}
