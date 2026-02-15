namespace Families.Core.Models;

public class Household
{
    public Guid HouseholdId { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? ZipCode { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Household() { }

    public Household(Guid tenantId, string name, string? address = null, string? city = null, string? state = null, string? zipCode = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        HouseholdId = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? name = null, string? address = null, string? city = null, string? state = null, string? zipCode = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }

        Address = address ?? Address;
        City = city ?? City;
        State = state ?? State;
        ZipCode = zipCode ?? ZipCode;
    }
}
