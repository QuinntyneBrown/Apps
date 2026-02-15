using MediatR;
using Microsoft.EntityFrameworkCore;
using Runs.Core;

namespace Runs.Api.Features;

public record UpdateRunCommand : IRequest<RunDto?>
{
    public Guid RunId { get; init; }
    public decimal Distance { get; init; }
    public int DurationMinutes { get; init; }
    public DateTime CompletedAt { get; init; }
    public int? AverageHeartRate { get; init; }
    public int? ElevationGain { get; init; }
    public int? CaloriesBurned { get; init; }
    public string? Route { get; init; }
    public string? Weather { get; init; }
    public string? Notes { get; init; }
    public int? EffortRating { get; init; }
}

public class UpdateRunCommandHandler : IRequestHandler<UpdateRunCommand, RunDto?>
{
    private readonly IRunsDbContext _context;
    public UpdateRunCommandHandler(IRunsDbContext context) => _context = context;

    public async Task<RunDto?> Handle(UpdateRunCommand request, CancellationToken cancellationToken)
    {
        var run = await _context.Runs.FirstOrDefaultAsync(r => r.RunId == request.RunId, cancellationToken);
        if (run == null) return null;

        run.Distance = request.Distance; run.DurationMinutes = request.DurationMinutes; run.CompletedAt = request.CompletedAt;
        run.AveragePace = request.Distance > 0 ? request.DurationMinutes / request.Distance : null;
        run.AverageHeartRate = request.AverageHeartRate; run.ElevationGain = request.ElevationGain;
        run.CaloriesBurned = request.CaloriesBurned; run.Route = request.Route; run.Weather = request.Weather;
        run.Notes = request.Notes; run.EffortRating = request.EffortRating;

        await _context.SaveChangesAsync(cancellationToken);

        return new RunDto
        {
            RunId = run.RunId, UserId = run.UserId, Distance = run.Distance, DurationMinutes = run.DurationMinutes,
            CompletedAt = run.CompletedAt, AveragePace = run.AveragePace, AverageHeartRate = run.AverageHeartRate,
            ElevationGain = run.ElevationGain, CaloriesBurned = run.CaloriesBurned, Route = run.Route,
            Weather = run.Weather, Notes = run.Notes, EffortRating = run.EffortRating
        };
    }
}
