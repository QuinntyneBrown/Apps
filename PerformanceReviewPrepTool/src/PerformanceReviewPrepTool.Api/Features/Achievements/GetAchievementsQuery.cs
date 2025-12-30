using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Achievements;

public record GetAchievementsQuery : IRequest<IEnumerable<AchievementDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ReviewPeriodId { get; init; }
    public bool? IsKeyAchievement { get; init; }
    public string? Category { get; init; }
}

public class GetAchievementsQueryHandler : IRequestHandler<GetAchievementsQuery, IEnumerable<AchievementDto>>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetAchievementsQueryHandler> _logger;

    public GetAchievementsQueryHandler(
        IPerformanceReviewPrepToolContext context,
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

        if (request.ReviewPeriodId.HasValue)
        {
            query = query.Where(a => a.ReviewPeriodId == request.ReviewPeriodId.Value);
        }

        if (request.IsKeyAchievement.HasValue)
        {
            query = query.Where(a => a.IsKeyAchievement == request.IsKeyAchievement.Value);
        }

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(a => a.Category == request.Category);
        }

        var achievements = await query
            .OrderByDescending(a => a.AchievedDate)
            .ToListAsync(cancellationToken);

        return achievements.Select(a => a.ToDto());
    }
}
