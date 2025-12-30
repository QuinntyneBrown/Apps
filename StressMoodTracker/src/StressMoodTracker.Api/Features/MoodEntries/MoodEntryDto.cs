using StressMoodTracker.Core;

namespace StressMoodTracker.Api.Features.MoodEntries;

public record MoodEntryDto
{
    public Guid MoodEntryId { get; init; }
    public Guid UserId { get; init; }
    public MoodLevel MoodLevel { get; init; }
    public StressLevel StressLevel { get; init; }
    public DateTime EntryTime { get; init; }
    public string? Notes { get; init; }
    public string? Activities { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class MoodEntryExtensions
{
    public static MoodEntryDto ToDto(this MoodEntry entry)
    {
        return new MoodEntryDto
        {
            MoodEntryId = entry.MoodEntryId,
            UserId = entry.UserId,
            MoodLevel = entry.MoodLevel,
            StressLevel = entry.StressLevel,
            EntryTime = entry.EntryTime,
            Notes = entry.Notes,
            Activities = entry.Activities,
            CreatedAt = entry.CreatedAt,
        };
    }
}
