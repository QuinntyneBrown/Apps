using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.Vehicles;

public record UpdateVehicleCommand : IRequest<VehicleDto?>
{
    public Guid VehicleId { get; init; }
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string? Trim { get; init; }
    public string? VIN { get; init; }
    public decimal CurrentMileage { get; init; }
    public decimal? PurchasePrice { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Color { get; init; }
    public string? InteriorType { get; init; }
    public string? EngineType { get; init; }
    public string? Transmission { get; init; }
    public bool IsCurrentlyOwned { get; init; }
    public string? Notes { get; init; }
}

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, VehicleDto?>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<UpdateVehicleCommandHandler> _logger;

    public UpdateVehicleCommandHandler(
        IVehicleValueTrackerContext context,
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
        vehicle.Trim = request.Trim;
        vehicle.VIN = request.VIN;
        vehicle.CurrentMileage = request.CurrentMileage;
        vehicle.PurchasePrice = request.PurchasePrice;
        vehicle.PurchaseDate = request.PurchaseDate;
        vehicle.Color = request.Color;
        vehicle.InteriorType = request.InteriorType;
        vehicle.EngineType = request.EngineType;
        vehicle.Transmission = request.Transmission;
        vehicle.IsCurrentlyOwned = request.IsCurrentlyOwned;
        vehicle.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated vehicle {VehicleId}", request.VehicleId);

        return vehicle.ToDto();
    }
}
