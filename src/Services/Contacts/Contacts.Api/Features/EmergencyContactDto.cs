using Contacts.Core.Models;

namespace Contacts.Api.Features;

public record EmergencyContactDto(Guid EmergencyContactId, Guid TenantId, string Name, string? Relationship, string PhoneNumber, string? AlternatePhone, string? Email, string? Address, bool IsPrimaryContact, string? ContactType, string? ServiceArea, string? Notes, bool IsActive);

public static class EmergencyContactExtensions
{
    public static EmergencyContactDto ToDto(this EmergencyContact c) => new(c.EmergencyContactId, c.TenantId, c.Name, c.Relationship, c.PhoneNumber, c.AlternatePhone, c.Email, c.Address, c.IsPrimaryContact, c.ContactType, c.ServiceArea, c.Notes, c.IsActive);
}
