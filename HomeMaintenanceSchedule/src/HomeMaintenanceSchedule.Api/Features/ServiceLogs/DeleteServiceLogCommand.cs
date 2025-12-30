using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.ServiceLogs;

public record DeleteServiceLogCommand : IRequest<bool>
{
    public Guid ServiceLogId { get; init; }
}

public class DeleteServiceLogCommandHandler : IRequestHandler<DeleteServiceLogCommand, bool>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<DeleteServiceLogCommandHandler> _logger;

    public DeleteServiceLogCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<DeleteServiceLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteServiceLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting service log {ServiceLogId}", request.ServiceLogId);

        var log = await _context.ServiceLogs
            .FirstOrDefaultAsync(l => l.ServiceLogId == request.ServiceLogId, cancellationToken);

        if (log == null)
        {
            _logger.LogWarning("Service log {ServiceLogId} not found", request.ServiceLogId);
            return false;
        }

        _context.ServiceLogs.Remove(log);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted service log {ServiceLogId}", request.ServiceLogId);

        return true;
    }
}
