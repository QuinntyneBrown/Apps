using ProfessionalReadingList.Core;

namespace ProfessionalReadingList.Api.Features.Notes;

public record NoteDto
{
    public Guid NoteId { get; init; }
    public Guid UserId { get; init; }
    public Guid ResourceId { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? PageReference { get; init; }
    public string? Quote { get; init; }
    public List<string> Tags { get; init; } = new List<string>();
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class NoteExtensions
{
    public static NoteDto ToDto(this Note note)
    {
        return new NoteDto
        {
            NoteId = note.NoteId,
            UserId = note.UserId,
            ResourceId = note.ResourceId,
            Content = note.Content,
            PageReference = note.PageReference,
            Quote = note.Quote,
            Tags = note.Tags,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt,
        };
    }
}
