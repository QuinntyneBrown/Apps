using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Achievements;

public record GetAchievementByIdQuery : IRequest<AchievementDto?>
{
    public Guid AchievementId { get; init; }
}

public class GetAchievementByIdQueryHandler : IRequestHandler<GetAchievementByIdQuery, AchievementDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetAchievementByIdQueryHandler> _logger;

    public GetAchievementByIdQueryHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<GetAchievementByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AchievementDto?> Handle(GetAchievementByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting achievement {AchievementId}", request.AchievementId);

        var achievement = await _context.Achievements
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AchievementId == request.AchievementId, cancellationToken);

        if (achievement == null)
        {
            _logger.LogWarning("Achievement {AchievementId} not found", request.AchievementId);
            return null;
        }

        return achievement.ToDto();
    }
}
