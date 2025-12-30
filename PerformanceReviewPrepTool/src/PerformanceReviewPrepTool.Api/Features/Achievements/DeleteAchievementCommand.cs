using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Achievements;

public record DeleteAchievementCommand : IRequest<bool>
{
    public Guid AchievementId { get; init; }
}

public class DeleteAchievementCommandHandler : IRequestHandler<DeleteAchievementCommand, bool>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<DeleteAchievementCommandHandler> _logger;

    public DeleteAchievementCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<DeleteAchievementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAchievementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting achievement {AchievementId}", request.AchievementId);

        var achievement = await _context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementId == request.AchievementId, cancellationToken);

        if (achievement == null)
        {
            _logger.LogWarning("Achievement {AchievementId} not found", request.AchievementId);
            return false;
        }

        _context.Achievements.Remove(achievement);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted achievement {AchievementId}", request.AchievementId);

        return true;
    }
}
