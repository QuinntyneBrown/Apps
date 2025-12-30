using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Notes;

public record UpdateNoteCommand : IRequest<NoteDto?>
{
    public Guid NoteId { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? PageReference { get; init; }
    public string? Quote { get; init; }
    public List<string> Tags { get; init; } = new List<string>();
}

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteDto?>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<UpdateNoteCommandHandler> _logger;

    public UpdateNoteCommandHandler(
        IProfessionalReadingListContext context,
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

        note.Content = request.Content;
        note.PageReference = request.PageReference;
        note.Quote = request.Quote;
        note.Tags = request.Tags;
        note.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated note {NoteId}", request.NoteId);

        return note.ToDto();
    }
}
