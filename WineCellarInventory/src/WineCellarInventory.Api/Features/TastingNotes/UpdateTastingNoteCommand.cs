using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.TastingNotes;

public record UpdateTastingNoteCommand : IRequest<TastingNoteDto?>
{
    public Guid TastingNoteId { get; init; }
    public DateTime TastingDate { get; init; }
    public int Rating { get; init; }
    public string? Appearance { get; init; }
    public string? Aroma { get; init; }
    public string? Taste { get; init; }
    public string? Finish { get; init; }
    public string? OverallImpression { get; init; }
}

public class UpdateTastingNoteCommandHandler : IRequestHandler<UpdateTastingNoteCommand, TastingNoteDto?>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<UpdateTastingNoteCommandHandler> _logger;

    public UpdateTastingNoteCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<UpdateTastingNoteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TastingNoteDto?> Handle(UpdateTastingNoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating tasting note {TastingNoteId}", request.TastingNoteId);

        var tastingNote = await _context.TastingNotes
            .FirstOrDefaultAsync(t => t.TastingNoteId == request.TastingNoteId, cancellationToken);

        if (tastingNote == null)
        {
            _logger.LogWarning("Tasting note {TastingNoteId} not found", request.TastingNoteId);
            return null;
        }

        tastingNote.TastingDate = request.TastingDate;
        tastingNote.Rating = request.Rating;
        tastingNote.Appearance = request.Appearance;
        tastingNote.Aroma = request.Aroma;
        tastingNote.Taste = request.Taste;
        tastingNote.Finish = request.Finish;
        tastingNote.OverallImpression = request.OverallImpression;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated tasting note {TastingNoteId}", request.TastingNoteId);

        return tastingNote.ToDto();
    }
}
