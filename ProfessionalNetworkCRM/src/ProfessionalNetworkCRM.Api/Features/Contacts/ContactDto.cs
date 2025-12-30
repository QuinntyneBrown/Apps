using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Api.Features.Contacts;

public record ContactDto
{
    public Guid ContactId { get; init; }
    public Guid UserId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public ContactType ContactType { get; init; }
    public string? Company { get; init; }
    public string? JobTitle { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? LinkedInUrl { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public List<string> Tags { get; init; } = new List<string>();
    public DateTime DateMet { get; init; }
    public DateTime? LastContactedDate { get; init; }
    public bool IsPriority { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ContactExtensions
{
    public static ContactDto ToDto(this Contact contact)
    {
        return new ContactDto
        {
            ContactId = contact.ContactId,
            UserId = contact.UserId,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            FullName = contact.FullName,
            ContactType = contact.ContactType,
            Company = contact.Company,
            JobTitle = contact.JobTitle,
            Email = contact.Email,
            Phone = contact.Phone,
            LinkedInUrl = contact.LinkedInUrl,
            Location = contact.Location,
            Notes = contact.Notes,
            Tags = contact.Tags,
            DateMet = contact.DateMet,
            LastContactedDate = contact.LastContactedDate,
            IsPriority = contact.IsPriority,
            CreatedAt = contact.CreatedAt,
            UpdatedAt = contact.UpdatedAt,
        };
    }
}
