using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Journals;

public record GetJournalsQuery : IRequest<IEnumerable<JournalDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Tag { get; init; }
}

public class GetJournalsQueryHandler : IRequestHandler<GetJournalsQuery, IEnumerable<JournalDto>>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<GetJournalsQueryHandler> _logger;

    public GetJournalsQueryHandler(
        IStressMoodTrackerContext context,
        ILogger<GetJournalsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<JournalDto>> Handle(GetJournalsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting journal entries for user {UserId}", request.UserId);

        var query = _context.Journals.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(j => j.UserId == request.UserId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(j => j.EntryDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(j => j.EntryDate <= request.EndDate.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Tag))
        {
            query = query.Where(j => j.Tags != null && j.Tags.Contains(request.Tag));
        }

        var journals = await query
            .OrderByDescending(j => j.EntryDate)
            .ToListAsync(cancellationToken);

        return journals.Select(j => j.ToDto());
    }
}
