using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.JournalEntries;

public record GetJournalEntriesQuery : IRequest<IEnumerable<JournalEntryDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public EntryType? EntryType { get; init; }
    public bool? IsSharedWithPartner { get; init; }
    public bool? IsPrivate { get; init; }
}

public class GetJournalEntriesQueryHandler : IRequestHandler<GetJournalEntriesQuery, IEnumerable<JournalEntryDto>>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<GetJournalEntriesQueryHandler> _logger;

    public GetJournalEntriesQueryHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<GetJournalEntriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<JournalEntryDto>> Handle(GetJournalEntriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting journal entries for user {UserId}", request.UserId);

        var query = _context.JournalEntries.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(e => e.UserId == request.UserId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(e => e.EntryDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(e => e.EntryDate <= request.EndDate.Value);
        }

        if (request.EntryType.HasValue)
        {
            query = query.Where(e => e.EntryType == request.EntryType.Value);
        }

        if (request.IsSharedWithPartner.HasValue)
        {
            query = query.Where(e => e.IsSharedWithPartner == request.IsSharedWithPartner.Value);
        }

        if (request.IsPrivate.HasValue)
        {
            query = query.Where(e => e.IsPrivate == request.IsPrivate.Value);
        }

        var entries = await query
            .OrderByDescending(e => e.EntryDate)
            .ToListAsync(cancellationToken);

        return entries.Select(e => e.ToDto());
    }
}
