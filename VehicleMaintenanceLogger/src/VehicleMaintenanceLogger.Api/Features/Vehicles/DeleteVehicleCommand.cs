using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.Vehicles;

public record DeleteVehicleCommand : IRequest<bool>
{
    public Guid VehicleId { get; init; }
}

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, bool>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<DeleteVehicleCommandHandler> _logger;

    public DeleteVehicleCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<DeleteVehicleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vehicle {VehicleId}", request.VehicleId);

        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

        if (vehicle == null)
        {
            _logger.LogWarning("Vehicle {VehicleId} not found", request.VehicleId);
            return false;
        }

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted vehicle {VehicleId}", request.VehicleId);

        return true;
    }
}
