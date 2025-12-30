using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.SleepSessions;

public record GetSleepSessionByIdQuery : IRequest<SleepSessionDto?>
{
    public Guid SleepSessionId { get; init; }
}

public class GetSleepSessionByIdQueryHandler : IRequestHandler<GetSleepSessionByIdQuery, SleepSessionDto?>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<GetSleepSessionByIdQueryHandler> _logger;

    public GetSleepSessionByIdQueryHandler(
        ISleepQualityTrackerContext context,
        ILogger<GetSleepSessionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SleepSessionDto?> Handle(GetSleepSessionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting sleep session {SleepSessionId}", request.SleepSessionId);

        var sleepSession = await _context.SleepSessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SleepSessionId == request.SleepSessionId, cancellationToken);

        if (sleepSession == null)
        {
            _logger.LogWarning("Sleep session {SleepSessionId} not found", request.SleepSessionId);
            return null;
        }

        return sleepSession.ToDto();
    }
}
