namespace Vehicles.Core.Models;

public class Vehicle
{
    public Guid VehicleId { get; set; }
    public Guid TenantId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? VIN { get; set; }
    public string? LicensePlate { get; set; }
    public string? Color { get; set; }
    public decimal? CurrentMileage { get; set; }
    public string? OwnerName { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}
