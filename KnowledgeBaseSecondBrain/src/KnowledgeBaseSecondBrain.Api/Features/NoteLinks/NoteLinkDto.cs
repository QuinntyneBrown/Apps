using KnowledgeBaseSecondBrain.Core;

namespace KnowledgeBaseSecondBrain.Api.Features.NoteLinks;

public record NoteLinkDto
{
    public Guid NoteLinkId { get; init; }
    public Guid SourceNoteId { get; init; }
    public Guid TargetNoteId { get; init; }
    public string? Description { get; init; }
    public string? LinkType { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class NoteLinkExtensions
{
    public static NoteLinkDto ToDto(this NoteLink noteLink)
    {
        return new NoteLinkDto
        {
            NoteLinkId = noteLink.NoteLinkId,
            SourceNoteId = noteLink.SourceNoteId,
            TargetNoteId = noteLink.TargetNoteId,
            Description = noteLink.Description,
            LinkType = noteLink.LinkType,
            CreatedAt = noteLink.CreatedAt,
        };
    }
}
