using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;

public record DeleteMaintenanceScheduleCommand : IRequest<bool>
{
    public Guid MaintenanceScheduleId { get; init; }
}

public class DeleteMaintenanceScheduleCommandHandler : IRequestHandler<DeleteMaintenanceScheduleCommand, bool>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<DeleteMaintenanceScheduleCommandHandler> _logger;

    public DeleteMaintenanceScheduleCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<DeleteMaintenanceScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting maintenance schedule {MaintenanceScheduleId}", request.MaintenanceScheduleId);

        var schedule = await _context.MaintenanceSchedules
            .FirstOrDefaultAsync(m => m.MaintenanceScheduleId == request.MaintenanceScheduleId, cancellationToken);

        if (schedule == null)
        {
            _logger.LogWarning("Maintenance schedule {MaintenanceScheduleId} not found", request.MaintenanceScheduleId);
            return false;
        }

        _context.MaintenanceSchedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted maintenance schedule {MaintenanceScheduleId}", request.MaintenanceScheduleId);

        return true;
    }
}
