using Achievements.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Achievements.Api.Features;

public record DeleteAchievementCommand(Guid AchievementId) : IRequest<bool>;

public class DeleteAchievementCommandHandler : IRequestHandler<DeleteAchievementCommand, bool>
{
    private readonly IAchievementsDbContext _context;
    private readonly ILogger<DeleteAchievementCommandHandler> _logger;

    public DeleteAchievementCommandHandler(IAchievementsDbContext context, ILogger<DeleteAchievementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement = await _context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementId == request.AchievementId, cancellationToken);

        if (achievement == null) return false;

        _context.Achievements.Remove(achievement);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Achievement deleted: {AchievementId}", request.AchievementId);

        return true;
    }
}
