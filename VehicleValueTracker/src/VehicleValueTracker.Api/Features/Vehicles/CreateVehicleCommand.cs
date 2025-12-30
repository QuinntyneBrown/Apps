using VehicleValueTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.Vehicles;

public record CreateVehicleCommand : IRequest<VehicleDto>
{
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
    public bool IsCurrentlyOwned { get; init; } = true;
    public string? Notes { get; init; }
}

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, VehicleDto>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<CreateVehicleCommandHandler> _logger;

    public CreateVehicleCommandHandler(
        IVehicleValueTrackerContext context,
        ILogger<CreateVehicleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating vehicle: {Year} {Make} {Model}",
            request.Year,
            request.Make,
            request.Model);

        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            Trim = request.Trim,
            VIN = request.VIN,
            CurrentMileage = request.CurrentMileage,
            PurchasePrice = request.PurchasePrice,
            PurchaseDate = request.PurchaseDate,
            Color = request.Color,
            InteriorType = request.InteriorType,
            EngineType = request.EngineType,
            Transmission = request.Transmission,
            IsCurrentlyOwned = request.IsCurrentlyOwned,
            Notes = request.Notes,
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created vehicle {VehicleId}: {Year} {Make} {Model}",
            vehicle.VehicleId,
            request.Year,
            request.Make,
            request.Model);

        return vehicle.ToDto();
    }
}
