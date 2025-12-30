using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.Notes;

public record CreateNoteCommand : IRequest<NoteDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public NoteType NoteType { get; init; }
    public Guid? ParentNoteId { get; init; }
}

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<CreateNoteCommandHandler> _logger;

    public CreateNoteCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<CreateNoteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating note for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            NoteType = request.NoteType,
            ParentNoteId = request.ParentNoteId,
            IsFavorite = false,
            IsArchived = false,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created note {NoteId} for user {UserId}",
            note.NoteId,
            request.UserId);

        return note.ToDto();
    }
}
