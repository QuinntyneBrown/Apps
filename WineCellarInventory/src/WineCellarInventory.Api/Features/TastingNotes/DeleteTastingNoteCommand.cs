using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.TastingNotes;

public record DeleteTastingNoteCommand : IRequest<bool>
{
    public Guid TastingNoteId { get; init; }
}

public class DeleteTastingNoteCommandHandler : IRequestHandler<DeleteTastingNoteCommand, bool>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<DeleteTastingNoteCommandHandler> _logger;

    public DeleteTastingNoteCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<DeleteTastingNoteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTastingNoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting tasting note {TastingNoteId}", request.TastingNoteId);

        var tastingNote = await _context.TastingNotes
            .FirstOrDefaultAsync(t => t.TastingNoteId == request.TastingNoteId, cancellationToken);

        if (tastingNote == null)
        {
            _logger.LogWarning("Tasting note {TastingNoteId} not found", request.TastingNoteId);
            return false;
        }

        _context.TastingNotes.Remove(tastingNote);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted tasting note {TastingNoteId}", request.TastingNoteId);

        return true;
    }
}
