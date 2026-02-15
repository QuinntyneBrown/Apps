using Goals.Core;
using Goals.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Goals.Api.Features;

public record CreateGoalCommand : IRequest<GoalDto>
{
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal TargetAmount { get; init; }
    public DateTime TargetDate { get; init; }
}

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly IGoalsDbContext _context;
    private readonly ILogger<CreateGoalCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateGoalCommandHandler(
        IGoalsDbContext context,
        ILogger<CreateGoalCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new Goal(
            request.TenantId,
            request.Name,
            request.TargetAmount,
            request.TargetDate,
            request.Description);

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishGoalCreatedEventAsync(goal, cancellationToken);

        _logger.LogInformation("Goal created: {GoalId}", goal.GoalId);

        return new GoalDto
        {
            GoalId = goal.GoalId,
            Name = goal.Name,
            Description = goal.Description,
            TargetAmount = goal.TargetAmount,
            CurrentAmount = goal.CurrentAmount,
            TargetDate = goal.TargetDate,
            Status = goal.Status.ToString(),
            CreatedAt = goal.CreatedAt
        };
    }

    private async Task PublishGoalCreatedEventAsync(Goal goal, CancellationToken cancellationToken)
    {
        if (_rabbitConnection == null) return;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("goals-events", ExchangeType.Topic, durable: true);

            var @event = new GoalCreatedEvent
            {
                GoalId = goal.GoalId,
                Name = goal.Name,
                TargetAmount = goal.TargetAmount,
                TenantId = goal.TenantId
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("goals-events", "goal.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish GoalCreatedEvent");
        }
    }
}
