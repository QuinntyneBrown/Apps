using WineCellarInventory.Core;

namespace WineCellarInventory.Api.Features.TastingNotes;

public record TastingNoteDto
{
    public Guid TastingNoteId { get; init; }
    public Guid UserId { get; init; }
    public Guid WineId { get; init; }
    public DateTime TastingDate { get; init; }
    public int Rating { get; init; }
    public string? Appearance { get; init; }
    public string? Aroma { get; init; }
    public string? Taste { get; init; }
    public string? Finish { get; init; }
    public string? OverallImpression { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class TastingNoteExtensions
{
    public static TastingNoteDto ToDto(this TastingNote tastingNote)
    {
        return new TastingNoteDto
        {
            TastingNoteId = tastingNote.TastingNoteId,
            UserId = tastingNote.UserId,
            WineId = tastingNote.WineId,
            TastingDate = tastingNote.TastingDate,
            Rating = tastingNote.Rating,
            Appearance = tastingNote.Appearance,
            Aroma = tastingNote.Aroma,
            Taste = tastingNote.Taste,
            Finish = tastingNote.Finish,
            OverallImpression = tastingNote.OverallImpression,
            CreatedAt = tastingNote.CreatedAt,
        };
    }
}
