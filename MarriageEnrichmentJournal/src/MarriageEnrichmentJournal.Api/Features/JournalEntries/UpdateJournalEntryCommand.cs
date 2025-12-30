using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.JournalEntries;

public record UpdateJournalEntryCommand : IRequest<JournalEntryDto?>
{
    public Guid JournalEntryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public EntryType EntryType { get; init; }
    public DateTime EntryDate { get; init; }
    public bool IsSharedWithPartner { get; init; }
    public bool IsPrivate { get; init; }
    public string? Tags { get; init; }
}

public class UpdateJournalEntryCommandHandler : IRequestHandler<UpdateJournalEntryCommand, JournalEntryDto?>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<UpdateJournalEntryCommandHandler> _logger;

    public UpdateJournalEntryCommandHandler(
        IMarriageEnrichmentJournalContext context,
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
        entry.EntryType = request.EntryType;
        entry.EntryDate = request.EntryDate;
        entry.IsSharedWithPartner = request.IsSharedWithPartner;
        entry.IsPrivate = request.IsPrivate;
        entry.Tags = request.Tags;
        entry.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated journal entry {JournalEntryId}", request.JournalEntryId);

        return entry.ToDto();
    }
}
