namespace Notes.Core.Models;

public class Note
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string? Tags { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? TenantId { get; set; }
}
