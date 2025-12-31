using ContactManagementApp.Core;

namespace ContactManagementApp.Api.Features.Contacts;

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
