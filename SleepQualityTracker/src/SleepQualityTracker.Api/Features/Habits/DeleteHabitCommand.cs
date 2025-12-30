using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Habits;

public record DeleteHabitCommand : IRequest<bool>
{
    public Guid HabitId { get; init; }
}

public class DeleteHabitCommandHandler : IRequestHandler<DeleteHabitCommand, bool>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<DeleteHabitCommandHandler> _logger;

    public DeleteHabitCommandHandler(
        ISleepQualityTrackerContext context,
        ILogger<DeleteHabitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting habit {HabitId}", request.HabitId);

        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.HabitId == request.HabitId, cancellationToken);

        if (habit == null)
        {
            _logger.LogWarning("Habit {HabitId} not found", request.HabitId);
            return false;
        }

        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted habit {HabitId}", request.HabitId);

        return true;
    }
}
