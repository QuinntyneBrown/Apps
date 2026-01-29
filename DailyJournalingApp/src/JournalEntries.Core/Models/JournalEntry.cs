namespace JournalEntries.Core.Models;

public class JournalEntry
{
    public Guid JournalEntryId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Mood { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
