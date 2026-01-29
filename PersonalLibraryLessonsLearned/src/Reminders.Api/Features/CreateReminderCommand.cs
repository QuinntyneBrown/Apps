using Reminders.Core;
using Reminders.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Reminders.Api.Features;

public record CreateReminderCommand(
    Guid UserId,
    Guid TenantId,
    Guid LessonId,
    DateTime ScheduledDate,
    string? Message) : IRequest<ReminderDto>;

public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, ReminderDto>
{
    private readonly IRemindersDbContext _context;
    private readonly ILogger<CreateReminderCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateReminderCommandHandler(
        IRemindersDbContext context,
        ILogger<CreateReminderCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ReminderDto> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            LessonId = request.LessonId,
            ScheduledDate = request.ScheduledDate,
            Message = request.Message,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishReminderScheduledEventAsync(reminder);

        _logger.LogInformation("Reminder created: {ReminderId}", reminder.ReminderId);

        return reminder.ToDto();
    }

    private Task PublishReminderScheduledEventAsync(Reminder reminder)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("reminders-events", ExchangeType.Topic, durable: true);

            var @event = new ReminderScheduledEvent
            {
                UserId = reminder.UserId,
                TenantId = reminder.TenantId,
                ReminderId = reminder.ReminderId,
                LessonId = reminder.LessonId,
                ScheduledDate = reminder.ScheduledDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("reminders-events", "reminder.scheduled", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ReminderScheduledEvent");
        }

        return Task.CompletedTask;
    }
}
