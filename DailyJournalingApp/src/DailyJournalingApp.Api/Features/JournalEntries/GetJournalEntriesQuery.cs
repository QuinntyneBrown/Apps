using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.JournalEntries;

public record GetJournalEntriesQuery : IRequest<IEnumerable<JournalEntryDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public Mood? Mood { get; init; }
    public bool? FavoritesOnly { get; init; }
}

public class GetJournalEntriesQueryHandler : IRequestHandler<GetJournalEntriesQuery, IEnumerable<JournalEntryDto>>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<GetJournalEntriesQueryHandler> _logger;

    public GetJournalEntriesQueryHandler(
        IDailyJournalingAppContext context,
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

        if (request.Mood.HasValue)
        {
            query = query.Where(e => e.Mood == request.Mood.Value);
        }

        if (request.FavoritesOnly == true)
        {
            query = query.Where(e => e.IsFavorite);
        }

        var entries = await query
            .OrderByDescending(e => e.EntryDate)
            .ToListAsync(cancellationToken);

        return entries.Select(e => e.ToDto());
    }
}
