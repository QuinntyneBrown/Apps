using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.SleepSessions;

public record DeleteSleepSessionCommand : IRequest<bool>
{
    public Guid SleepSessionId { get; init; }
}

public class DeleteSleepSessionCommandHandler : IRequestHandler<DeleteSleepSessionCommand, bool>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<DeleteSleepSessionCommandHandler> _logger;

    public DeleteSleepSessionCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<DeleteSleepSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSleepSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting sleep session {SleepSessionId}", request.SleepSessionId);

        var sleepSession = await _context.SleepSessions
            .FirstOrDefaultAsync(s => s.SleepSessionId == request.SleepSessionId, cancellationToken);

        if (sleepSession == null)
        {
            _logger.LogWarning("Sleep session {SleepSessionId} not found", request.SleepSessionId);
            return false;
        }

        _context.SleepSessions.Remove(sleepSession);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted sleep session {SleepSessionId}", request.SleepSessionId);

        return true;
    }
}
