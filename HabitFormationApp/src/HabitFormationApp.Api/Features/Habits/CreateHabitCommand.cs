using HabitFormationApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HabitFormationApp.Api.Features.Habits;

public record CreateHabitCommand : IRequest<HabitDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public HabitFrequency Frequency { get; init; }
    public int TargetDaysPerWeek { get; init; } = 7;
    public DateTime? StartDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand, HabitDto>
{
    private readonly IHabitFormationAppContext _context;
    private readonly ILogger<CreateHabitCommandHandler> _logger;

    public CreateHabitCommandHandler(
        IHabitFormationAppContext context,
        ILogger<CreateHabitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HabitDto> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating habit for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Frequency = request.Frequency,
            TargetDaysPerWeek = request.TargetDaysPerWeek,
            StartDate = request.StartDate ?? DateTime.UtcNow,
            IsActive = true,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created habit {HabitId} for user {UserId}",
            habit.HabitId,
            request.UserId);

        return habit.ToDto();
    }
}
