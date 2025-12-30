using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.JournalEntries;

public record GetJournalEntryByIdQuery : IRequest<JournalEntryDto?>
{
    public Guid JournalEntryId { get; init; }
}

public class GetJournalEntryByIdQueryHandler : IRequestHandler<GetJournalEntryByIdQuery, JournalEntryDto?>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<GetJournalEntryByIdQueryHandler> _logger;

    public GetJournalEntryByIdQueryHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<GetJournalEntryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<JournalEntryDto?> Handle(GetJournalEntryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting journal entry {JournalEntryId}", request.JournalEntryId);

        var entry = await _context.JournalEntries
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.JournalEntryId == request.JournalEntryId, cancellationToken);

        return entry?.ToDto();
    }
}
