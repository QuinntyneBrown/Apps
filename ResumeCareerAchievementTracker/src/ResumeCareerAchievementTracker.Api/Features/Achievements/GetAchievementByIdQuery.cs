using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Achievements;

public record GetAchievementByIdQuery : IRequest<AchievementDto?>
{
    public Guid AchievementId { get; init; }
}

public class GetAchievementByIdQueryHandler : IRequestHandler<GetAchievementByIdQuery, AchievementDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<GetAchievementByIdQueryHandler> _logger;

    public GetAchievementByIdQueryHandler(
        IResumeCareerAchievementTrackerContext context,
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

        return achievement?.ToDto();
    }
}
