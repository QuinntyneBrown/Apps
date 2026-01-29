namespace Appliances.Core.Models;

public class Appliance
{
    public Guid ApplianceId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ApplianceType ApplianceType { get; set; }
    public string? Brand { get; set; }
    public string? ModelNumber { get; set; }
    public string? SerialNumber { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum ApplianceType
{
    Refrigerator,
    Washer,
    Dryer,
    Dishwasher,
    Oven,
    Microwave,
    AirConditioner,
    Heater,
    WaterHeater,
    Other
}
