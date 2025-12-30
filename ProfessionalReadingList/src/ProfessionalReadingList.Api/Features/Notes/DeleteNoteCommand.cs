using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Notes;

public record DeleteNoteCommand : IRequest<bool>
{
    public Guid NoteId { get; init; }
}

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<DeleteNoteCommandHandler> _logger;

    public DeleteNoteCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<DeleteNoteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting note {NoteId}", request.NoteId);

        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken);

        if (note == null)
        {
            _logger.LogWarning("Note {NoteId} not found", request.NoteId);
            return false;
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted note {NoteId}", request.NoteId);

        return true;
    }
}
