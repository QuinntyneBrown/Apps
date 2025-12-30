using KnowledgeBaseSecondBrain.Core;

namespace KnowledgeBaseSecondBrain.Api.Features.Notes;

public record NoteDto
{
    public Guid NoteId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public NoteType NoteType { get; init; }
    public Guid? ParentNoteId { get; init; }
    public bool IsFavorite { get; init; }
    public bool IsArchived { get; init; }
    public DateTime LastModifiedAt { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class NoteExtensions
{
    public static NoteDto ToDto(this Note note)
    {
        return new NoteDto
        {
            NoteId = note.NoteId,
            UserId = note.UserId,
            Title = note.Title,
            Content = note.Content,
            NoteType = note.NoteType,
            ParentNoteId = note.ParentNoteId,
            IsFavorite = note.IsFavorite,
            IsArchived = note.IsArchived,
            LastModifiedAt = note.LastModifiedAt,
            CreatedAt = note.CreatedAt,
        };
    }
}
