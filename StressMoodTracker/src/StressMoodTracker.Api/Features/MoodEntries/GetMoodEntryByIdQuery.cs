using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.MoodEntries;

public record GetMoodEntryByIdQuery : IRequest<MoodEntryDto?>
{
    public Guid MoodEntryId { get; init; }
}

public class GetMoodEntryByIdQueryHandler : IRequestHandler<GetMoodEntryByIdQuery, MoodEntryDto?>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<GetMoodEntryByIdQueryHandler> _logger;

    public GetMoodEntryByIdQueryHandler(
        IStressMoodTrackerContext context,
        ILogger<GetMoodEntryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MoodEntryDto?> Handle(GetMoodEntryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting mood entry {MoodEntryId}", request.MoodEntryId);

        var entry = await _context.MoodEntries
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.MoodEntryId == request.MoodEntryId, cancellationToken);

        return entry?.ToDto();
    }
}
