using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.MaintenanceTasks;

public record DeleteMaintenanceTaskCommand : IRequest<bool>
{
    public Guid MaintenanceTaskId { get; init; }
}

public class DeleteMaintenanceTaskCommandHandler : IRequestHandler<DeleteMaintenanceTaskCommand, bool>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<DeleteMaintenanceTaskCommandHandler> _logger;

    public DeleteMaintenanceTaskCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<DeleteMaintenanceTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMaintenanceTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting maintenance task {MaintenanceTaskId}", request.MaintenanceTaskId);

        var task = await _context.MaintenanceTasks
            .FirstOrDefaultAsync(t => t.MaintenanceTaskId == request.MaintenanceTaskId, cancellationToken);

        if (task == null)
        {
            _logger.LogWarning("Maintenance task {MaintenanceTaskId} not found", request.MaintenanceTaskId);
            return false;
        }

        _context.MaintenanceTasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted maintenance task {MaintenanceTaskId}", request.MaintenanceTaskId);

        return true;
    }
}
