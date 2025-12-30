using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.JournalEntries;

public record ToggleFavoriteCommand : IRequest<JournalEntryDto?>
{
    public Guid JournalEntryId { get; init; }
}

public class ToggleFavoriteCommandHandler : IRequestHandler<ToggleFavoriteCommand, JournalEntryDto?>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<ToggleFavoriteCommandHandler> _logger;

    public ToggleFavoriteCommandHandler(
        IDailyJournalingAppContext context,
        ILogger<ToggleFavoriteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<JournalEntryDto?> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Toggling favorite for journal entry {JournalEntryId}", request.JournalEntryId);

        var entry = await _context.JournalEntries
            .FirstOrDefaultAsync(e => e.JournalEntryId == request.JournalEntryId, cancellationToken);

        if (entry == null)
        {
            _logger.LogWarning("Journal entry {JournalEntryId} not found", request.JournalEntryId);
            return null;
        }

        entry.IsFavorite = !entry.IsFavorite;
        entry.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Toggled favorite for journal entry {JournalEntryId} to {IsFavorite}",
            request.JournalEntryId,
            entry.IsFavorite);

        return entry.ToDto();
    }
}
