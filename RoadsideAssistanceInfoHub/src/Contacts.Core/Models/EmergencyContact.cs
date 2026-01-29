namespace Contacts.Core.Models;

public class EmergencyContact
{
    public Guid EmergencyContactId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Relationship { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? AlternatePhone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsPrimaryContact { get; set; }
    public string? ContactType { get; set; }
    public string? ServiceArea { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}
