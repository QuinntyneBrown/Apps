using MediatR;
using Microsoft.EntityFrameworkCore;
using Races.Core;

namespace Races.Api.Features;

public record GetRacesQuery : IRequest<IEnumerable<RaceDto>>;

public class GetRacesQueryHandler : IRequestHandler<GetRacesQuery, IEnumerable<RaceDto>>
{
    private readonly IRacesDbContext _context;

    public GetRacesQueryHandler(IRacesDbContext context) => _context = context;

    public async Task<IEnumerable<RaceDto>> Handle(GetRacesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Races
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
            .ToListAsync(cancellationToken);
    }
}
