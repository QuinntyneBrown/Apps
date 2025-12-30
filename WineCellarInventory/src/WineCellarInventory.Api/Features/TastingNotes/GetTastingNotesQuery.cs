using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.TastingNotes;

public record GetTastingNotesQuery : IRequest<IEnumerable<TastingNoteDto>>
{
    public Guid? UserId { get; init; }
    public Guid? WineId { get; init; }
    public int? MinRating { get; init; }
}

public class GetTastingNotesQueryHandler : IRequestHandler<GetTastingNotesQuery, IEnumerable<TastingNoteDto>>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<GetTastingNotesQueryHandler> _logger;

    public GetTastingNotesQueryHandler(
        IWineCellarInventoryContext context,
        ILogger<GetTastingNotesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TastingNoteDto>> Handle(GetTastingNotesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tasting notes for user {UserId}", request.UserId);

        var query = _context.TastingNotes.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.WineId.HasValue)
        {
            query = query.Where(t => t.WineId == request.WineId.Value);
        }

        if (request.MinRating.HasValue)
        {
            query = query.Where(t => t.Rating >= request.MinRating.Value);
        }

        var tastingNotes = await query
            .OrderByDescending(t => t.TastingDate)
            .ToListAsync(cancellationToken);

        return tastingNotes.Select(t => t.ToDto());
    }
}
