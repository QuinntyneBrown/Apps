using Carpools.Core;
using Carpools.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Carpools.Api.Features;

public record CreateCarpoolCommand(
    Guid UserId,
    Guid TenantId,
    Guid ScheduleId,
    string DriverName,
    string? DriverPhone,
    int AvailableSeats,
    string? PickupLocation,
    TimeSpan? PickupTime,
    string? Notes) : IRequest<CarpoolDto>;

public class CreateCarpoolCommandHandler : IRequestHandler<CreateCarpoolCommand, CarpoolDto>
{
    private readonly ICarpoolsDbContext _context;
    private readonly ILogger<CreateCarpoolCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateCarpoolCommandHandler(
        ICarpoolsDbContext context,
        ILogger<CreateCarpoolCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<CarpoolDto> Handle(CreateCarpoolCommand request, CancellationToken cancellationToken)
    {
        var carpool = new Carpool
        {
            CarpoolId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            ScheduleId = request.ScheduleId,
            DriverName = request.DriverName,
            DriverPhone = request.DriverPhone,
            AvailableSeats = request.AvailableSeats,
            PickupLocation = request.PickupLocation,
            PickupTime = request.PickupTime,
            Notes = request.Notes,
            IsConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Carpools.Add(carpool);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishCarpoolCreatedEventAsync(carpool);

        _logger.LogInformation("Carpool created: {CarpoolId}", carpool.CarpoolId);

        return carpool.ToDto();
    }

    private Task PublishCarpoolCreatedEventAsync(Carpool carpool)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("carpools-events", ExchangeType.Topic, durable: true);

            var @event = new CarpoolCreatedEvent
            {
                UserId = carpool.UserId,
                TenantId = carpool.TenantId,
                CarpoolId = carpool.CarpoolId,
                ScheduleId = carpool.ScheduleId,
                DriverName = carpool.DriverName
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("carpools-events", "carpool.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish CarpoolCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
