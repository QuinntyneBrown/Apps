using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.SleepSessions;

public record UpdateSleepSessionCommand : IRequest<SleepSessionDto?>
{
    public Guid SleepSessionId { get; init; }
    public DateTime Bedtime { get; init; }
    public DateTime WakeTime { get; init; }
    public int TotalSleepMinutes { get; init; }
    public SleepQuality SleepQuality { get; init; }
    public int? TimesAwakened { get; init; }
    public int? DeepSleepMinutes { get; init; }
    public int? RemSleepMinutes { get; init; }
    public decimal? SleepEfficiency { get; init; }
    public string? Notes { get; init; }
}

public class UpdateSleepSessionCommandHandler : IRequestHandler<UpdateSleepSessionCommand, SleepSessionDto?>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<UpdateSleepSessionCommandHandler> _logger;

    public UpdateSleepSessionCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<UpdateSleepSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SleepSessionDto?> Handle(UpdateSleepSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating sleep session {SleepSessionId}", request.SleepSessionId);

        var sleepSession = await _context.SleepSessions
            .FirstOrDefaultAsync(s => s.SleepSessionId == request.SleepSessionId, cancellationToken);

        if (sleepSession == null)
        {
            _logger.LogWarning("Sleep session {SleepSessionId} not found", request.SleepSessionId);
            return null;
        }

        sleepSession.Bedtime = request.Bedtime;
        sleepSession.WakeTime = request.WakeTime;
        sleepSession.TotalSleepMinutes = request.TotalSleepMinutes;
        sleepSession.SleepQuality = request.SleepQuality;
        sleepSession.TimesAwakened = request.TimesAwakened;
        sleepSession.DeepSleepMinutes = request.DeepSleepMinutes;
        sleepSession.RemSleepMinutes = request.RemSleepMinutes;
        sleepSession.SleepEfficiency = request.SleepEfficiency;
        sleepSession.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated sleep session {SleepSessionId}", request.SleepSessionId);

        return sleepSession.ToDto();
    }
}
