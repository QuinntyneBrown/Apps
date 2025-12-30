using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Journals;

public record UpdateJournalCommand : IRequest<JournalDto?>
{
    public Guid JournalId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime EntryDate { get; init; }
    public string? Tags { get; init; }
}

public class UpdateJournalCommandHandler : IRequestHandler<UpdateJournalCommand, JournalDto?>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<UpdateJournalCommandHandler> _logger;

    public UpdateJournalCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<UpdateJournalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<JournalDto?> Handle(UpdateJournalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating journal entry {JournalId}", request.JournalId);

        var journal = await _context.Journals
            .FirstOrDefaultAsync(j => j.JournalId == request.JournalId, cancellationToken);

        if (journal == null)
        {
            _logger.LogWarning("Journal entry {JournalId} not found", request.JournalId);
            return null;
        }

        journal.Title = request.Title;
        journal.Content = request.Content;
        journal.EntryDate = request.EntryDate;
        journal.Tags = request.Tags;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated journal entry {JournalId}", request.JournalId);

        return journal.ToDto();
    }
}
