using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Goals;

public record CreateGoalCommand : IRequest<GoalDto>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public GoalType GoalType { get; init; }
    public decimal TargetAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<CreateGoalCommandHandler> _logger;

    public CreateGoalCommandHandler(
        IFinancialGoalTrackerContext context,
        ILogger<CreateGoalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating goal: {Name}, type: {GoalType}",
            request.Name,
            request.GoalType);

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            GoalType = request.GoalType,
            TargetAmount = request.TargetAmount,
            CurrentAmount = 0,
            TargetDate = request.TargetDate,
            Status = GoalStatus.NotStarted,
            Notes = request.Notes,
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created goal {GoalId}: {Name}",
            goal.GoalId,
            request.Name);

        return goal.ToDto();
    }
}
