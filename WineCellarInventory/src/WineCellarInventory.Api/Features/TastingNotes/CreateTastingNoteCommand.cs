using WineCellarInventory.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.TastingNotes;

public record CreateTastingNoteCommand : IRequest<TastingNoteDto>
{
    public Guid UserId { get; init; }
    public Guid WineId { get; init; }
    public DateTime TastingDate { get; init; } = DateTime.UtcNow;
    public int Rating { get; init; }
    public string? Appearance { get; init; }
    public string? Aroma { get; init; }
    public string? Taste { get; init; }
    public string? Finish { get; init; }
    public string? OverallImpression { get; init; }
}

public class CreateTastingNoteCommandHandler : IRequestHandler<CreateTastingNoteCommand, TastingNoteDto>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<CreateTastingNoteCommandHandler> _logger;

    public CreateTastingNoteCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<CreateTastingNoteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TastingNoteDto> Handle(CreateTastingNoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating tasting note for user {UserId}, wine: {WineId}",
            request.UserId,
            request.WineId);

        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = request.UserId,
            WineId = request.WineId,
            TastingDate = request.TastingDate,
            Rating = request.Rating,
            Appearance = request.Appearance,
            Aroma = request.Aroma,
            Taste = request.Taste,
            Finish = request.Finish,
            OverallImpression = request.OverallImpression,
            CreatedAt = DateTime.UtcNow,
        };

        _context.TastingNotes.Add(tastingNote);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created tasting note {TastingNoteId} for wine {WineId}",
            tastingNote.TastingNoteId,
            request.WineId);

        return tastingNote.ToDto();
    }
}
