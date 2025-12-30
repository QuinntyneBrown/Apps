using MarriageEnrichmentJournal.Core;

namespace MarriageEnrichmentJournal.Api.Features.Reflections;

public record ReflectionDto
{
    public Guid ReflectionId { get; init; }
    public Guid? JournalEntryId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = string.Empty;
    public string? Topic { get; init; }
    public DateTime ReflectionDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ReflectionExtensions
{
    public static ReflectionDto ToDto(this Reflection reflection)
    {
        return new ReflectionDto
        {
            ReflectionId = reflection.ReflectionId,
            JournalEntryId = reflection.JournalEntryId,
            UserId = reflection.UserId,
            Text = reflection.Text,
            Topic = reflection.Topic,
            ReflectionDate = reflection.ReflectionDate,
            CreatedAt = reflection.CreatedAt,
            UpdatedAt = reflection.UpdatedAt,
        };
    }
}
