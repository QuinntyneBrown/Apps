using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.JournalEntries;

public record DeleteJournalEntryCommand : IRequest<bool>
{
    public Guid JournalEntryId { get; init; }
}

public class DeleteJournalEntryCommandHandler : IRequestHandler<DeleteJournalEntryCommand, bool>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<DeleteJournalEntryCommandHandler> _logger;

    public DeleteJournalEntryCommandHandler(
        IDailyJournalingAppContext context,
        ILogger<DeleteJournalEntryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteJournalEntryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting journal entry {JournalEntryId}", request.JournalEntryId);

        var entry = await _context.JournalEntries
            .FirstOrDefaultAsync(e => e.JournalEntryId == request.JournalEntryId, cancellationToken);

        if (entry == null)
        {
            _logger.LogWarning("Journal entry {JournalEntryId} not found", request.JournalEntryId);
            return false;
        }

        _context.JournalEntries.Remove(entry);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted journal entry {JournalEntryId}", request.JournalEntryId);

        return true;
    }
}
