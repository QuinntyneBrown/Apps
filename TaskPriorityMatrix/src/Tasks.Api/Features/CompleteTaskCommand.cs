using Tasks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Tasks.Api.Features;

public record CompleteTaskCommand(Guid PriorityTaskId) : IRequest<TaskDto?>;

public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, TaskDto?>
{
    private readonly ITasksDbContext _context;
    private readonly ILogger<CompleteTaskCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CompleteTaskCommandHandler(
        ITasksDbContext context,
        ILogger<CompleteTaskCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<TaskDto?> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.PriorityTaskId == request.PriorityTaskId, cancellationToken);

        if (task == null) return null;

        task.Complete();
        await _context.SaveChangesAsync(cancellationToken);

        await PublishTaskCompletedEventAsync(task);

        _logger.LogInformation("Task completed: {TaskId}", task.PriorityTaskId);

        return task.ToDto();
    }

    private Task PublishTaskCompletedEventAsync(Tasks.Core.Models.PriorityTask task)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("tasks-events", ExchangeType.Topic, durable: true);

            var @event = new TaskCompletedEvent
            {
                UserId = task.UserId,
                TenantId = task.TenantId,
                TaskId = task.PriorityTaskId,
                CompletedAt = task.CompletedAt!.Value
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("tasks-events", "task.completed", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish TaskCompletedEvent");
        }

        return Task.CompletedTask;
    }
}
