using MediatR;
using Microsoft.EntityFrameworkCore;
using Runs.Core;

namespace Runs.Api.Features;

public record GetRunsQuery : IRequest<IEnumerable<RunDto>>;

public class GetRunsQueryHandler : IRequestHandler<GetRunsQuery, IEnumerable<RunDto>>
{
    private readonly IRunsDbContext _context;
    public GetRunsQueryHandler(IRunsDbContext context) => _context = context;

    public async Task<IEnumerable<RunDto>> Handle(GetRunsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Runs.Select(r => new RunDto
        {
            RunId = r.RunId, UserId = r.UserId, Distance = r.Distance, DurationMinutes = r.DurationMinutes,
            CompletedAt = r.CompletedAt, AveragePace = r.AveragePace, AverageHeartRate = r.AverageHeartRate,
            ElevationGain = r.ElevationGain, CaloriesBurned = r.CaloriesBurned, Route = r.Route,
            Weather = r.Weather, Notes = r.Notes, EffortRating = r.EffortRating
        }).ToListAsync(cancellationToken);
    }
}
