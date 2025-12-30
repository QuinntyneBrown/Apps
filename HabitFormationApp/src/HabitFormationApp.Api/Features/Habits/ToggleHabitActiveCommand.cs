using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Habits;

public record ToggleHabitActiveCommand : IRequest<HabitDto?>
{
    public Guid HabitId { get; init; }
}

public class ToggleHabitActiveCommandHandler : IRequestHandler<ToggleHabitActiveCommand, HabitDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<ToggleHabitActiveCommandHandler> _logger;

    public ToggleHabitActiveCommandHandler(
        IHabitFormationAppContext context,
        ILogger<ToggleHabitActiveCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HabitDto?> Handle(ToggleHabitActiveCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Toggling active status for habit {HabitId}", request.HabitId);

        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.HabitId == request.HabitId, cancellationToken);

        if (habit == null)
        {
            _logger.LogWarning("Habit {HabitId} not found", request.HabitId);
            return null;
        }

        habit.ToggleActive();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Toggled active status for habit {HabitId} to {IsActive}",
            request.HabitId,
            habit.IsActive);

        return habit.ToDto();
    }
}
