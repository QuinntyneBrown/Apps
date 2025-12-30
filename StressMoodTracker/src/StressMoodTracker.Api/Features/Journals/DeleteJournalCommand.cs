using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Journals;

public record DeleteJournalCommand : IRequest<bool>
{
    public Guid JournalId { get; init; }
}

public class DeleteJournalCommandHandler : IRequestHandler<DeleteJournalCommand, bool>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<DeleteJournalCommandHandler> _logger;

    public DeleteJournalCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<DeleteJournalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteJournalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting journal entry {JournalId}", request.JournalId);

        var journal = await _context.Journals
            .FirstOrDefaultAsync(j => j.JournalId == request.JournalId, cancellationToken);

        if (journal == null)
        {
            _logger.LogWarning("Journal entry {JournalId} not found", request.JournalId);
            return false;
        }

        _context.Journals.Remove(journal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted journal entry {JournalId}", request.JournalId);

        return true;
    }
}
