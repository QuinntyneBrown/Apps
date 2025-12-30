using StressMoodTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.MoodEntries;

public record CreateMoodEntryCommand : IRequest<MoodEntryDto>
{
    public Guid UserId { get; init; }
    public MoodLevel MoodLevel { get; init; }
    public StressLevel StressLevel { get; init; }
    public DateTime EntryTime { get; init; }
    public string? Notes { get; init; }
    public string? Activities { get; init; }
}

public class CreateMoodEntryCommandHandler : IRequestHandler<CreateMoodEntryCommand, MoodEntryDto>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<CreateMoodEntryCommandHandler> _logger;

    public CreateMoodEntryCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<CreateMoodEntryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MoodEntryDto> Handle(CreateMoodEntryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating mood entry for user {UserId}, mood: {MoodLevel}, stress: {StressLevel}",
            request.UserId,
            request.MoodLevel,
            request.StressLevel);

        var entry = new MoodEntry
        {
            MoodEntryId = Guid.NewGuid(),
            UserId = request.UserId,
            MoodLevel = request.MoodLevel,
            StressLevel = request.StressLevel,
            EntryTime = request.EntryTime,
            Notes = request.Notes,
            Activities = request.Activities,
            CreatedAt = DateTime.UtcNow,
        };

        _context.MoodEntries.Add(entry);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created mood entry {MoodEntryId} for user {UserId}",
            entry.MoodEntryId,
            request.UserId);

        return entry.ToDto();
    }
}
