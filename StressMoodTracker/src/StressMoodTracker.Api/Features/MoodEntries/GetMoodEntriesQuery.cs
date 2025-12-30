using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.MoodEntries;

public record GetMoodEntriesQuery : IRequest<IEnumerable<MoodEntryDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public MoodLevel? MoodLevel { get; init; }
    public StressLevel? StressLevel { get; init; }
}

public class GetMoodEntriesQueryHandler : IRequestHandler<GetMoodEntriesQuery, IEnumerable<MoodEntryDto>>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<GetMoodEntriesQueryHandler> _logger;

    public GetMoodEntriesQueryHandler(
        IStressMoodTrackerContext context,
        ILogger<GetMoodEntriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MoodEntryDto>> Handle(GetMoodEntriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting mood entries for user {UserId}", request.UserId);

        var query = _context.MoodEntries.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(e => e.UserId == request.UserId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(e => e.EntryTime >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(e => e.EntryTime <= request.EndDate.Value);
        }

        if (request.MoodLevel.HasValue)
        {
            query = query.Where(e => e.MoodLevel == request.MoodLevel.Value);
        }

        if (request.StressLevel.HasValue)
        {
            query = query.Where(e => e.StressLevel == request.StressLevel.Value);
        }

        var entries = await query
            .OrderByDescending(e => e.EntryTime)
            .ToListAsync(cancellationToken);

        return entries.Select(e => e.ToDto());
    }
}
