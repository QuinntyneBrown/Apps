using MediatR;
using Microsoft.EntityFrameworkCore;
using Runs.Core;

namespace Runs.Api.Features;

public record GetRunByIdQuery(Guid RunId) : IRequest<RunDto?>;

public class GetRunByIdQueryHandler : IRequestHandler<GetRunByIdQuery, RunDto?>
{
    private readonly IRunsDbContext _context;
    public GetRunByIdQueryHandler(IRunsDbContext context) => _context = context;

    public async Task<RunDto?> Handle(GetRunByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Runs.Where(r => r.RunId == request.RunId).Select(r => new RunDto
        {
            RunId = r.RunId, UserId = r.UserId, Distance = r.Distance, DurationMinutes = r.DurationMinutes,
            CompletedAt = r.CompletedAt, AveragePace = r.AveragePace, AverageHeartRate = r.AverageHeartRate,
            ElevationGain = r.ElevationGain, CaloriesBurned = r.CaloriesBurned, Route = r.Route,
            Weather = r.Weather, Notes = r.Notes, EffortRating = r.EffortRating
        }).FirstOrDefaultAsync(cancellationToken);
    }
}
