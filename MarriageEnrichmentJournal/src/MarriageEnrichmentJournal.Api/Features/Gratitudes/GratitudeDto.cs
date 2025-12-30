using MarriageEnrichmentJournal.Core;

namespace MarriageEnrichmentJournal.Api.Features.Gratitudes;

public record GratitudeDto
{
    public Guid GratitudeId { get; init; }
    public Guid? JournalEntryId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = string.Empty;
    public DateTime GratitudeDate { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class GratitudeExtensions
{
    public static GratitudeDto ToDto(this Gratitude gratitude)
    {
        return new GratitudeDto
        {
            GratitudeId = gratitude.GratitudeId,
            JournalEntryId = gratitude.JournalEntryId,
            UserId = gratitude.UserId,
            Text = gratitude.Text,
            GratitudeDate = gratitude.GratitudeDate,
            CreatedAt = gratitude.CreatedAt,
        };
    }
}
