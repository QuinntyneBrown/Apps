using Goals.Core;
using Goals.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Goals.Api.Features;

public record CreateGoalCommand(
    Guid TenantId,
    Guid UserId,
    string Category,
    int TargetMinutesPerDay,
    string? Notes) : IRequest<GoalDto>;

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
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            TenantId = request.TenantId,
            UserId = request.UserId,
            Category = request.Category,
            TargetMinutesPerDay = request.TargetMinutesPerDay,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishGoalCreatedEventAsync(goal);

        _logger.LogInformation("Goal created: {GoalId}", goal.GoalId);

        return goal.ToDto();
    }

    private Task PublishGoalCreatedEventAsync(Goal goal)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("goals-events", ExchangeType.Topic, durable: true);

            var @event = new GoalCreatedEvent
            {
                UserId = goal.UserId,
                TenantId = goal.TenantId,
                GoalId = goal.GoalId,
                Category = goal.Category,
                TargetMinutesPerDay = goal.TargetMinutesPerDay
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("goals-events", "goal.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish GoalCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
