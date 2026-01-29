using PaymentSchedules.Core;
using PaymentSchedules.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace PaymentSchedules.Api.Features;

public record CreatePaymentScheduleCommand(
    Guid OfferId,
    Guid TenantId,
    int PaymentNumber,
    DateTime DueDate,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal TotalPayment,
    decimal RemainingBalance) : IRequest<PaymentScheduleDto>;

public class CreatePaymentScheduleCommandHandler : IRequestHandler<CreatePaymentScheduleCommand, PaymentScheduleDto>
{
    private readonly IPaymentSchedulesDbContext _context;
    private readonly ILogger<CreatePaymentScheduleCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreatePaymentScheduleCommandHandler(
        IPaymentSchedulesDbContext context,
        ILogger<CreatePaymentScheduleCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<PaymentScheduleDto> Handle(CreatePaymentScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = new PaymentSchedule
        {
            ScheduleId = Guid.NewGuid(),
            OfferId = request.OfferId,
            TenantId = request.TenantId,
            PaymentNumber = request.PaymentNumber,
            DueDate = request.DueDate,
            PrincipalAmount = request.PrincipalAmount,
            InterestAmount = request.InterestAmount,
            TotalPayment = request.TotalPayment,
            RemainingBalance = request.RemainingBalance,
            CreatedAt = DateTime.UtcNow
        };

        _context.PaymentSchedules.Add(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishScheduleGeneratedEventAsync(schedule);

        _logger.LogInformation("PaymentSchedule created: {ScheduleId}", schedule.ScheduleId);

        return schedule.ToDto();
    }

    private Task PublishScheduleGeneratedEventAsync(PaymentSchedule schedule)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("paymentschedules-events", ExchangeType.Topic, durable: true);

            var @event = new ScheduleGeneratedEvent
            {
                TenantId = schedule.TenantId,
                ScheduleId = schedule.ScheduleId,
                OfferId = schedule.OfferId,
                TotalPayments = schedule.PaymentNumber
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("paymentschedules-events", "schedule.generated", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ScheduleGeneratedEvent");
        }

        return Task.CompletedTask;
    }
}
