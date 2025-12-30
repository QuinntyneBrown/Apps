using DailyJournalingApp.Core;

namespace DailyJournalingApp.Api.Features.JournalEntries;

public record JournalEntryDto
{
    public Guid JournalEntryId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime EntryDate { get; init; }
    public Mood Mood { get; init; }
    public Guid? PromptId { get; init; }
    public string? Tags { get; init; }
    public bool IsFavorite { get; init; }
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
            EntryDate = entry.EntryDate,
            Mood = entry.Mood,
            PromptId = entry.PromptId,
            Tags = entry.Tags,
            IsFavorite = entry.IsFavorite,
            CreatedAt = entry.CreatedAt,
            UpdatedAt = entry.UpdatedAt,
        };
    }
}
