namespace Vehicles.Core.Models;

public class Vehicle
{
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Vin { get; set; }
    public int? Mileage { get; set; }
    public string? Condition { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
