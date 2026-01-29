namespace Contacts.Core.Models;

public class Contact
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string? JobTitle { get; set; }
    public string? Email { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? TenantId { get; set; }
}
