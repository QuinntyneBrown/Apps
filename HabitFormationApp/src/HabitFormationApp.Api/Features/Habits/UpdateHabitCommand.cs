using HabitFormationApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Habits;

public record UpdateHabitCommand : IRequest<HabitDto?>
{
    public Guid HabitId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public HabitFrequency Frequency { get; init; }
    public int TargetDaysPerWeek { get; init; }
    public string? Notes { get; init; }
}

public class UpdateHabitCommandHandler : IRequestHandler<UpdateHabitCommand, HabitDto?>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<UpdateHabitCommandHandler> _logger;

    public UpdateHabitCommandHandler(
        IHabitFormationAppContext context,
        ILogger<UpdateHabitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HabitDto?> Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating habit {HabitId}", request.HabitId);

        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.HabitId == request.HabitId, cancellationToken);

        if (habit == null)
        {
            _logger.LogWarning("Habit {HabitId} not found", request.HabitId);
            return null;
        }

        habit.Name = request.Name;
        habit.Description = request.Description;
        habit.Frequency = request.Frequency;
        habit.TargetDaysPerWeek = request.TargetDaysPerWeek;
        habit.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated habit {HabitId}", request.HabitId);

        return habit.ToDto();
    }
}
