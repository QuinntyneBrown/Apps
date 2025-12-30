using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.Notes;

public record UpdateNoteCommand : IRequest<NoteDto?>
{
    public Guid NoteId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public NoteType NoteType { get; init; }
    public Guid? ParentNoteId { get; init; }
    public bool IsFavorite { get; init; }
    public bool IsArchived { get; init; }
}

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteDto?>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<UpdateNoteCommandHandler> _logger;

    public UpdateNoteCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<UpdateNoteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteDto?> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating note {NoteId}", request.NoteId);

        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken);

        if (note == null)
        {
            _logger.LogWarning("Note {NoteId} not found", request.NoteId);
            return null;
        }

        note.Title = request.Title;
        note.Content = request.Content;
        note.NoteType = request.NoteType;
        note.ParentNoteId = request.ParentNoteId;
        note.IsFavorite = request.IsFavorite;
        note.IsArchived = request.IsArchived;
        note.LastModifiedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated note {NoteId}", request.NoteId);

        return note.ToDto();
    }
}
