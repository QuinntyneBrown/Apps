using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.Vehicles;

public record CreateVehicleCommand : IRequest<VehicleDto>
{
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string? VIN { get; init; }
    public string? LicensePlate { get; init; }
    public VehicleType VehicleType { get; init; }
    public decimal CurrentMileage { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, VehicleDto>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<CreateVehicleCommandHandler> _logger;

    public CreateVehicleCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<CreateVehicleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating vehicle: {Make} {Model} {Year}",
            request.Make,
            request.Model,
            request.Year);

        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            VIN = request.VIN,
            LicensePlate = request.LicensePlate,
            VehicleType = request.VehicleType,
            CurrentMileage = request.CurrentMileage,
            PurchaseDate = request.PurchaseDate,
            Notes = request.Notes,
            IsActive = true,
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created vehicle {VehicleId}: {Make} {Model}",
            vehicle.VehicleId,
            vehicle.Make,
            vehicle.Model);

        return vehicle.ToDto();
    }
}
