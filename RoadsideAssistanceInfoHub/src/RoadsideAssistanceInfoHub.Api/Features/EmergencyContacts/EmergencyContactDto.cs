using RoadsideAssistanceInfoHub.Core;

namespace RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;

public record EmergencyContactDto
{
    public Guid EmergencyContactId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Relationship { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string? AlternatePhone { get; init; }
    public string? Email { get; init; }
    public string? Address { get; init; }
    public bool IsPrimaryContact { get; init; }
    public string? ContactType { get; init; }
    public string? ServiceArea { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
}

public static class EmergencyContactExtensions
{
    public static EmergencyContactDto ToDto(this EmergencyContact contact)
    {
        return new EmergencyContactDto
        {
            EmergencyContactId = contact.EmergencyContactId,
            Name = contact.Name,
            Relationship = contact.Relationship,
            PhoneNumber = contact.PhoneNumber,
            AlternatePhone = contact.AlternatePhone,
            Email = contact.Email,
            Address = contact.Address,
            IsPrimaryContact = contact.IsPrimaryContact,
            ContactType = contact.ContactType,
            ServiceArea = contact.ServiceArea,
            Notes = contact.Notes,
            IsActive = contact.IsActive,
        };
    }
}
