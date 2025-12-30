using SleepQualityTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.SleepSessions;

public record CreateSleepSessionCommand : IRequest<SleepSessionDto>
{
    public Guid UserId { get; init; }
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

public class CreateSleepSessionCommandHandler : IRequestHandler<CreateSleepSessionCommand, SleepSessionDto>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<CreateSleepSessionCommandHandler> _logger;

    public CreateSleepSessionCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<CreateSleepSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SleepSessionDto> Handle(CreateSleepSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating sleep session for user {UserId}, quality: {SleepQuality}",
            request.UserId,
            request.SleepQuality);

        var sleepSession = new SleepSession
        {
            SleepSessionId = Guid.NewGuid(),
            UserId = request.UserId,
            Bedtime = request.Bedtime,
            WakeTime = request.WakeTime,
            TotalSleepMinutes = request.TotalSleepMinutes,
            SleepQuality = request.SleepQuality,
            TimesAwakened = request.TimesAwakened,
            DeepSleepMinutes = request.DeepSleepMinutes,
            RemSleepMinutes = request.RemSleepMinutes,
            SleepEfficiency = request.SleepEfficiency,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.SleepSessions.Add(sleepSession);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created sleep session {SleepSessionId} for user {UserId}",
            sleepSession.SleepSessionId,
            request.UserId);

        return sleepSession.ToDto();
    }
}
