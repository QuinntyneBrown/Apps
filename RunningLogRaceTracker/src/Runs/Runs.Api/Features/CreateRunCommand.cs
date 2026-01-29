using MediatR;
using Runs.Core;
using Runs.Core.Models;

namespace Runs.Api.Features;

public record CreateRunCommand : IRequest<RunDto>
{
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
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

public class CreateRunCommandHandler : IRequestHandler<CreateRunCommand, RunDto>
{
    private readonly IRunsDbContext _context;
    public CreateRunCommandHandler(IRunsDbContext context) => _context = context;

    public async Task<RunDto> Handle(CreateRunCommand request, CancellationToken cancellationToken)
    {
        var run = new Run
        {
            RunId = Guid.NewGuid(), UserId = request.UserId, TenantId = request.TenantId,
            Distance = request.Distance, DurationMinutes = request.DurationMinutes, CompletedAt = request.CompletedAt,
            AveragePace = request.Distance > 0 ? request.DurationMinutes / request.Distance : null,
            AverageHeartRate = request.AverageHeartRate, ElevationGain = request.ElevationGain,
            CaloriesBurned = request.CaloriesBurned, Route = request.Route, Weather = request.Weather,
            Notes = request.Notes, EffortRating = request.EffortRating, CreatedAt = DateTime.UtcNow
        };

        _context.Runs.Add(run);
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
