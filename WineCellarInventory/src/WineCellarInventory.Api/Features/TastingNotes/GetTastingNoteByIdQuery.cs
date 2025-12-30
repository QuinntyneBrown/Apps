using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.TastingNotes;

public record GetTastingNoteByIdQuery : IRequest<TastingNoteDto?>
{
    public Guid TastingNoteId { get; init; }
}

public class GetTastingNoteByIdQueryHandler : IRequestHandler<GetTastingNoteByIdQuery, TastingNoteDto?>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<GetTastingNoteByIdQueryHandler> _logger;

    public GetTastingNoteByIdQueryHandler(
        IWineCellarInventoryContext context,
        ILogger<GetTastingNoteByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TastingNoteDto?> Handle(GetTastingNoteByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tasting note {TastingNoteId}", request.TastingNoteId);

        var tastingNote = await _context.TastingNotes
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TastingNoteId == request.TastingNoteId, cancellationToken);

        if (tastingNote == null)
        {
            _logger.LogWarning("Tasting note {TastingNoteId} not found", request.TastingNoteId);
            return null;
        }

        return tastingNote.ToDto();
    }
}
