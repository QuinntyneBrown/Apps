using Schedules.Core;
using Schedules.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Schedules.Api.Features;

public record CreateScheduleCommand(
    Guid UserId,
    Guid TenantId,
    Guid ActivityId,
    DateTime EventDate,
    TimeSpan StartTime,
    TimeSpan? EndTime,
    string? Location,
    string? Notes,
    bool IsRecurring,
    string? RecurrencePattern) : IRequest<ScheduleDto>;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ScheduleDto>
{
    private readonly ISchedulesDbContext _context;
    private readonly ILogger<CreateScheduleCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateScheduleCommandHandler(
        ISchedulesDbContext context,
        ILogger<CreateScheduleCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ScheduleDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = new Schedule
        {
            ScheduleId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            ActivityId = request.ActivityId,
            EventDate = request.EventDate,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            Notes = request.Notes,
            IsRecurring = request.IsRecurring,
            RecurrencePattern = request.RecurrencePattern,
            CreatedAt = DateTime.UtcNow
        };

        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishScheduleCreatedEventAsync(schedule);

        _logger.LogInformation("Schedule created: {ScheduleId}", schedule.ScheduleId);

        return schedule.ToDto();
    }

    private Task PublishScheduleCreatedEventAsync(Schedule schedule)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("schedules-events", ExchangeType.Topic, durable: true);

            var @event = new ScheduleCreatedEvent
            {
                UserId = schedule.UserId,
                TenantId = schedule.TenantId,
                ScheduleId = schedule.ScheduleId,
                ActivityId = schedule.ActivityId,
                EventDate = schedule.EventDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("schedules-events", "schedule.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ScheduleCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
