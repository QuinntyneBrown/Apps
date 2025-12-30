using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.MoodEntries;

public record DeleteMoodEntryCommand : IRequest<bool>
{
    public Guid MoodEntryId { get; init; }
}

public class DeleteMoodEntryCommandHandler : IRequestHandler<DeleteMoodEntryCommand, bool>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<DeleteMoodEntryCommandHandler> _logger;

    public DeleteMoodEntryCommandHandler(
        IStressMoodTrackerContext context,
        ILogger<DeleteMoodEntryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMoodEntryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting mood entry {MoodEntryId}", request.MoodEntryId);

        var entry = await _context.MoodEntries
            .FirstOrDefaultAsync(e => e.MoodEntryId == request.MoodEntryId, cancellationToken);

        if (entry == null)
        {
            _logger.LogWarning("Mood entry {MoodEntryId} not found", request.MoodEntryId);
            return false;
        }

        _context.MoodEntries.Remove(entry);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted mood entry {MoodEntryId}", request.MoodEntryId);

        return true;
    }
}
