using MarriageEnrichmentJournal.Core;

namespace MarriageEnrichmentJournal.Api.Features.JournalEntries;

public record JournalEntryDto
{
    public Guid JournalEntryId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public EntryType EntryType { get; init; }
    public DateTime EntryDate { get; init; }
    public bool IsSharedWithPartner { get; init; }
    public bool IsPrivate { get; init; }
    public string? Tags { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class JournalEntryExtensions
{
    public static JournalEntryDto ToDto(this JournalEntry entry)
    {
        return new JournalEntryDto
        {
            JournalEntryId = entry.JournalEntryId,
            UserId = entry.UserId,
            Title = entry.Title,
            Content = entry.Content,
            EntryType = entry.EntryType,
            EntryDate = entry.EntryDate,
            IsSharedWithPartner = entry.IsSharedWithPartner,
            IsPrivate = entry.IsPrivate,
            Tags = entry.Tags,
            CreatedAt = entry.CreatedAt,
            UpdatedAt = entry.UpdatedAt,
        };
    }
}
