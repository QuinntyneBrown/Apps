using VehicleValueTracker.Core;

namespace VehicleValueTracker.Api.Features.Vehicles;

public record VehicleDto
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
            Trim = vehicle.Trim,
            VIN = vehicle.VIN,
            CurrentMileage = vehicle.CurrentMileage,
            PurchasePrice = vehicle.PurchasePrice,
            PurchaseDate = vehicle.PurchaseDate,
            Color = vehicle.Color,
            InteriorType = vehicle.InteriorType,
            EngineType = vehicle.EngineType,
            Transmission = vehicle.Transmission,
            IsCurrentlyOwned = vehicle.IsCurrentlyOwned,
            Notes = vehicle.Notes,
        };
    }
}
