using Vehicles.Core;
using Vehicles.Core.Models;
using MediatR;

namespace Vehicles.Api.Features;

public record CreateVehicleCommand(Guid TenantId, string Make, string Model, int Year, string? VIN, string? LicensePlate, string? Color, decimal? CurrentMileage, string? OwnerName, string? Notes) : IRequest<VehicleDto>;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, VehicleDto>
{
    private readonly IVehiclesDbContext _context;
    public CreateVehicleCommandHandler(IVehiclesDbContext context) => _context = context;
    public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken ct)
    {
        var vehicle = new Vehicle { VehicleId = Guid.NewGuid(), TenantId = request.TenantId, Make = request.Make, Model = request.Model, Year = request.Year, VIN = request.VIN, LicensePlate = request.LicensePlate, Color = request.Color, CurrentMileage = request.CurrentMileage, OwnerName = request.OwnerName, Notes = request.Notes, IsActive = true };
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync(ct);
        return vehicle.ToDto();
    }
}
