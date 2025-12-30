using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.Vehicles;

public record UpdateVehicleCommand : IRequest<VehicleDto?>
{
    public Guid VehicleId { get; init; }
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string? VIN { get; init; }
    public string? LicensePlate { get; init; }
    public VehicleType VehicleType { get; init; }
    public decimal CurrentMileage { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, VehicleDto?>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<UpdateVehicleCommandHandler> _logger;

    public UpdateVehicleCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<UpdateVehicleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VehicleDto?> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating vehicle {VehicleId}", request.VehicleId);

        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

        if (vehicle == null)
        {
            _logger.LogWarning("Vehicle {VehicleId} not found", request.VehicleId);
            return null;
        }

        vehicle.Make = request.Make;
        vehicle.Model = request.Model;
        vehicle.Year = request.Year;
        vehicle.VIN = request.VIN;
        vehicle.LicensePlate = request.LicensePlate;
        vehicle.VehicleType = request.VehicleType;
        vehicle.CurrentMileage = request.CurrentMileage;
        vehicle.PurchaseDate = request.PurchaseDate;
        vehicle.Notes = request.Notes;
        vehicle.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated vehicle {VehicleId}", request.VehicleId);

        return vehicle.ToDto();
    }
}
