using Achievements.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Achievements.Api.Features;

public record GetAchievementByIdQuery(Guid AchievementId) : IRequest<AchievementDto?>;

public class GetAchievementByIdQueryHandler : IRequestHandler<GetAchievementByIdQuery, AchievementDto?>
{
    private readonly IAchievementsDbContext _context;

    public GetAchievementByIdQueryHandler(IAchievementsDbContext context)
    {
        _context = context;
    }

    public async Task<AchievementDto?> Handle(GetAchievementByIdQuery request, CancellationToken cancellationToken)
    {
        var achievement = await _context.Achievements
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AchievementId == request.AchievementId, cancellationToken);

        return achievement?.ToDto();
    }
}
