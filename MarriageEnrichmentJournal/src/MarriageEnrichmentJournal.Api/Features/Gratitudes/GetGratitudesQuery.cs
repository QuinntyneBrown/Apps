using MarriageEnrichmentJournal.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarriageEnrichmentJournal.Api.Features.Gratitudes;

public record GetGratitudesQuery : IRequest<IEnumerable<GratitudeDto>>
{
    public Guid? UserId { get; init; }
    public Guid? JournalEntryId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetGratitudesQueryHandler : IRequestHandler<GetGratitudesQuery, IEnumerable<GratitudeDto>>
{
    private readonly IMarriageEnrichmentJournalContext _context;
    private readonly ILogger<GetGratitudesQueryHandler> _logger;

    public GetGratitudesQueryHandler(
        IMarriageEnrichmentJournalContext context,
        ILogger<GetGratitudesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GratitudeDto>> Handle(GetGratitudesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gratitudes for user {UserId}", request.UserId);

        var query = _context.Gratitudes.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        if (request.JournalEntryId.HasValue)
        {
            query = query.Where(g => g.JournalEntryId == request.JournalEntryId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(g => g.GratitudeDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(g => g.GratitudeDate <= request.EndDate.Value);
        }

        var gratitudes = await query
            .OrderByDescending(g => g.GratitudeDate)
            .ToListAsync(cancellationToken);

        return gratitudes.Select(g => g.ToDto());
    }
}
