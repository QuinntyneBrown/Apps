using Tasks.Core;
using Tasks.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Tasks.Api.Features;

public record CreateTaskCommand(
    Guid TenantId,
    Guid UserId,
    string Title,
    string Description,
    Urgency Urgency,
    Importance Importance,
    DateTime? DueDate,
    Guid? CategoryId) : IRequest<TaskDto>;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly ITasksDbContext _context;
    private readonly ILogger<CreateTaskCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateTaskCommandHandler(
        ITasksDbContext context,
        ILogger<CreateTaskCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new PriorityTask
        {
            PriorityTaskId = Guid.NewGuid(),
            TenantId = request.TenantId,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Urgency = request.Urgency,
            Importance = request.Importance,
            DueDate = request.DueDate,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishTaskCreatedEventAsync(task);

        _logger.LogInformation("Task created: {TaskId}", task.PriorityTaskId);

        return task.ToDto();
    }

    private Task PublishTaskCreatedEventAsync(PriorityTask task)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("tasks-events", ExchangeType.Topic, durable: true);

            var @event = new TaskCreatedEvent
            {
                UserId = task.UserId,
                TenantId = task.TenantId,
                TaskId = task.PriorityTaskId,
                Title = task.Title,
                Urgency = task.Urgency.ToString(),
                Importance = task.Importance.ToString()
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("tasks-events", "task.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish TaskCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
