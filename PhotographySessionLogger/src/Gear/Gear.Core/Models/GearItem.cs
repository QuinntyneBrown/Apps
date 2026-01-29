namespace Gear.Core.Models;

public class GearItem
{
    public Guid GearItemId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string? Brand { get; private set; }
    public string? Model { get; private set; }
    public string? SerialNumber { get; private set; }
    public DateTime? PurchaseDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private GearItem() { }

    public GearItem(Guid tenantId, Guid userId, string name, string type, string? brand = null, string? model = null)
    {
        GearItemId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        Type = type;
        Brand = brand;
        Model = model;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string type, string? brand = null, string? model = null, string? serialNumber = null)
    {
        Name = name;
        Type = type;
        Brand = brand;
        Model = model;
        SerialNumber = serialNumber;
        UpdatedAt = DateTime.UtcNow;
    }
}
