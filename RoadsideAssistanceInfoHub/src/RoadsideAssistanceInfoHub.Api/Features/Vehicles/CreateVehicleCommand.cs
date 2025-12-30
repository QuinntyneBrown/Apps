using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.Vehicles;

public record CreateVehicleCommand : IRequest<VehicleDto>
{
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string? VIN { get; init; }
    public string? LicensePlate { get; init; }
    public string? Color { get; init; }
    public decimal? CurrentMileage { get; init; }
    public string? OwnerName { get; init; }
    public string? Notes { get; init; }
}

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, VehicleDto>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<CreateVehicleCommandHandler> _logger;

    public CreateVehicleCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
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
            Color = request.Color,
            CurrentMileage = request.CurrentMileage,
            OwnerName = request.OwnerName,
            Notes = request.Notes,
            IsActive = true,
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created vehicle {VehicleId}",
            vehicle.VehicleId);

        return vehicle.ToDto();
    }
}
