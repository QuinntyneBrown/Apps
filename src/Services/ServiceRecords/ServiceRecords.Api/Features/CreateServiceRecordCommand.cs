using ServiceRecords.Core;
using ServiceRecords.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace ServiceRecords.Api.Features;

public record CreateServiceRecordCommand(
    Guid TenantId,
    Guid ApplianceId,
    DateTime ServiceDate,
    string? ServiceProvider,
    string? Description,
    decimal? Cost) : IRequest<ServiceRecordDto>;

public class CreateServiceRecordCommandHandler : IRequestHandler<CreateServiceRecordCommand, ServiceRecordDto>
{
    private readonly IServiceRecordsDbContext _context;
    private readonly ILogger<CreateServiceRecordCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateServiceRecordCommandHandler(
        IServiceRecordsDbContext context,
        ILogger<CreateServiceRecordCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ServiceRecordDto> Handle(CreateServiceRecordCommand request, CancellationToken cancellationToken)
    {
        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            TenantId = request.TenantId,
            ApplianceId = request.ApplianceId,
            ServiceDate = request.ServiceDate,
            ServiceProvider = request.ServiceProvider,
            Description = request.Description,
            Cost = request.Cost,
            CreatedAt = DateTime.UtcNow
        };

        _context.ServiceRecords.Add(serviceRecord);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishServiceRecordCreatedEventAsync(serviceRecord);

        _logger.LogInformation("ServiceRecord created: {ServiceRecordId}", serviceRecord.ServiceRecordId);

        return serviceRecord.ToDto();
    }

    private Task PublishServiceRecordCreatedEventAsync(ServiceRecord sr)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("servicerecords-events", ExchangeType.Topic, durable: true);

            var @event = new ServiceRecordCreatedEvent
            {
                TenantId = sr.TenantId,
                ServiceRecordId = sr.ServiceRecordId,
                ApplianceId = sr.ApplianceId,
                ServiceDate = sr.ServiceDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("servicerecords-events", "servicerecord.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ServiceRecordCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
