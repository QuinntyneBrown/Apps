using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.MoodEntries;

public record UpdateMoodEntryCommand : IRequest<MoodEntryDto?>
{
    public Guid MoodEntryId { get; init; }
    public MoodLevel MoodLevel { get; init; }
    public StressLevel StressLevel { get; init; }
    public DateTime EntryTime { get; init; }
    public string? Notes { get; init; }
    public string? Activities { get; init; }
}

public class UpdateMoodEntryCommandHandler : IRequestHandler<UpdateMoodEntryCommand, MoodEntryDto?>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<UpdateMoodEntryCommandHandler> _logger;

    public UpdateMoodEntryCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<UpdateMoodEntryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MoodEntryDto?> Handle(UpdateMoodEntryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating mood entry {MoodEntryId}", request.MoodEntryId);

        var entry = await _context.MoodEntries
            .FirstOrDefaultAsync(e => e.MoodEntryId == request.MoodEntryId, cancellationToken);

        if (entry == null)
        {
            _logger.LogWarning("Mood entry {MoodEntryId} not found", request.MoodEntryId);
            return null;
        }

        entry.MoodLevel = request.MoodLevel;
        entry.StressLevel = request.StressLevel;
        entry.EntryTime = request.EntryTime;
        entry.Notes = request.Notes;
        entry.Activities = request.Activities;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated mood entry {MoodEntryId}", request.MoodEntryId);

        return entry.ToDto();
    }
}
