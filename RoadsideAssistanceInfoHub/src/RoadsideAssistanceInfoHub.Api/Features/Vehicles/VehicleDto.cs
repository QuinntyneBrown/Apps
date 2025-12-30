using RoadsideAssistanceInfoHub.Core;

namespace RoadsideAssistanceInfoHub.Api.Features.Vehicles;

public record VehicleDto
{
    public Guid VehicleId { get; init; }
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public string? VIN { get; init; }
    public string? LicensePlate { get; init; }
    public string? Color { get; init; }
    public decimal? CurrentMileage { get; init; }
    public string? OwnerName { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
}

public static class VehicleExtensions
{
    public static VehicleDto ToDto(this Vehicle vehicle)
    {
        return new VehicleDto
        {
            VehicleId = vehicle.VehicleId,
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = vehicle.Year,
            VIN = vehicle.VIN,
            LicensePlate = vehicle.LicensePlate,
            Color = vehicle.Color,
            CurrentMileage = vehicle.CurrentMileage,
            OwnerName = vehicle.OwnerName,
            Notes = vehicle.Notes,
            IsActive = vehicle.IsActive,
        };
    }
}
