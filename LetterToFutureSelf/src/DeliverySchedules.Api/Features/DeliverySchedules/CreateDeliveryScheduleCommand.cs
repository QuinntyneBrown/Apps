using DeliverySchedules.Core;
using DeliverySchedules.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace DeliverySchedules.Api.Features.DeliverySchedules;

public record CreateDeliveryScheduleCommand : IRequest<DeliveryScheduleDto>
{
    public Guid TenantId { get; init; }
    public Guid LetterId { get; init; }
    public DateTime ScheduledDateTime { get; init; }
    public string DeliveryMethod { get; init; } = string.Empty;
    public string? RecipientContact { get; init; }
}

public class CreateDeliveryScheduleCommandHandler : IRequestHandler<CreateDeliveryScheduleCommand, DeliveryScheduleDto>
{
    private readonly IDeliverySchedulesDbContext _context;
    private readonly ILogger<CreateDeliveryScheduleCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateDeliveryScheduleCommandHandler(
        IDeliverySchedulesDbContext context,
        ILogger<CreateDeliveryScheduleCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<DeliveryScheduleDto> Handle(CreateDeliveryScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = new DeliverySchedule
        {
            DeliveryScheduleId = Guid.NewGuid(),
            TenantId = request.TenantId,
            LetterId = request.LetterId,
            ScheduledDateTime = request.ScheduledDateTime,
            DeliveryMethod = request.DeliveryMethod,
            RecipientContact = request.RecipientContact,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.DeliverySchedules.Add(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishDeliveryScheduleCreatedEventAsync(schedule, cancellationToken);

        _logger.LogInformation("Delivery schedule created: {DeliveryScheduleId}", schedule.DeliveryScheduleId);

        return new DeliveryScheduleDto
        {
            DeliveryScheduleId = schedule.DeliveryScheduleId,
            LetterId = schedule.LetterId,
            ScheduledDateTime = schedule.ScheduledDateTime,
            DeliveryMethod = schedule.DeliveryMethod,
            RecipientContact = schedule.RecipientContact,
            IsActive = schedule.IsActive,
            CreatedAt = schedule.CreatedAt
        };
    }

    private async Task PublishDeliveryScheduleCreatedEventAsync(DeliverySchedule schedule, CancellationToken cancellationToken)
    {
        if (_rabbitConnection == null) return;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("deliveryschedules-events", ExchangeType.Topic, durable: true);

            var @event = new DeliveryScheduleCreatedEvent
            {
                TenantId = schedule.TenantId,
                DeliveryScheduleId = schedule.DeliveryScheduleId,
                LetterId = schedule.LetterId,
                ScheduledDateTime = schedule.ScheduledDateTime,
                DeliveryMethod = schedule.DeliveryMethod
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("deliveryschedules-events", "deliveryschedule.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish DeliveryScheduleCreatedEvent");
        }
    }
}
