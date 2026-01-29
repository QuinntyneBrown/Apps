namespace Recipients.Core.Models;

public class Recipient
{
    public Guid RecipientId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Relationship { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Interests { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
