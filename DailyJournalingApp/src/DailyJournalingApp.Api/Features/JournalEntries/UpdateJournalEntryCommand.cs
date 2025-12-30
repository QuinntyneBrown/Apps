using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.JournalEntries;

public record UpdateJournalEntryCommand : IRequest<JournalEntryDto?>
{
    public Guid JournalEntryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime EntryDate { get; init; }
    public Mood Mood { get; init; }
    public string? Tags { get; init; }
}

public class UpdateJournalEntryCommandHandler : IRequestHandler<UpdateJournalEntryCommand, JournalEntryDto?>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<UpdateJournalEntryCommandHandler> _logger;

    public UpdateJournalEntryCommandHandler(
        IDailyJournalingAppContext context,
        ILogger<UpdateJournalEntryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<JournalEntryDto?> Handle(UpdateJournalEntryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating journal entry {JournalEntryId}", request.JournalEntryId);

        var entry = await _context.JournalEntries
            .FirstOrDefaultAsync(e => e.JournalEntryId == request.JournalEntryId, cancellationToken);

        if (entry == null)
        {
            _logger.LogWarning("Journal entry {JournalEntryId} not found", request.JournalEntryId);
            return null;
        }

        entry.Title = request.Title;
        entry.Content = request.Content;
        entry.EntryDate = request.EntryDate;
        entry.Mood = request.Mood;
        entry.Tags = request.Tags;
        entry.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated journal entry {JournalEntryId}", request.JournalEntryId);

        return entry.ToDto();
    }
}
