using Achievements.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Achievements.Api.Features;

public record GetAchievementsQuery : IRequest<IEnumerable<AchievementDto>>;

public class GetAchievementsQueryHandler : IRequestHandler<GetAchievementsQuery, IEnumerable<AchievementDto>>
{
    private readonly IAchievementsDbContext _context;

    public GetAchievementsQueryHandler(IAchievementsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AchievementDto>> Handle(GetAchievementsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Achievements
            .AsNoTracking()
            .Select(a => a.ToDto())
            .ToListAsync(cancellationToken);
    }
}
