using StressMoodTracker.Core;

namespace StressMoodTracker.Api.Features.Journals;

public record JournalDto
{
    public Guid JournalId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime EntryDate { get; init; }
    public string? Tags { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class JournalExtensions
{
    public static JournalDto ToDto(this Journal journal)
    {
        return new JournalDto
        {
            JournalId = journal.JournalId,
            UserId = journal.UserId,
            Title = journal.Title,
            Content = journal.Content,
            EntryDate = journal.EntryDate,
            Tags = journal.Tags,
            CreatedAt = journal.CreatedAt,
        };
    }
}
