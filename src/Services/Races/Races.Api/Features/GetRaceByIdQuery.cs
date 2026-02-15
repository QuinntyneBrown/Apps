using MediatR;
using Microsoft.EntityFrameworkCore;
using Races.Core;

namespace Races.Api.Features;

public record GetRaceByIdQuery(Guid RaceId) : IRequest<RaceDto?>;

public class GetRaceByIdQueryHandler : IRequestHandler<GetRaceByIdQuery, RaceDto?>
{
    private readonly IRacesDbContext _context;

    public GetRaceByIdQueryHandler(IRacesDbContext context) => _context = context;

    public async Task<RaceDto?> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Races
            .Where(r => r.RaceId == request.RaceId)
            .Select(r => new RaceDto
            {
                RaceId = r.RaceId,
                UserId = r.UserId,
                Name = r.Name,
                RaceType = r.RaceType,
                RaceDate = r.RaceDate,
                Location = r.Location,
                Distance = r.Distance,
                FinishTimeMinutes = r.FinishTimeMinutes,
                GoalTimeMinutes = r.GoalTimeMinutes,
                Placement = r.Placement,
                IsCompleted = r.IsCompleted,
                Notes = r.Notes
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
