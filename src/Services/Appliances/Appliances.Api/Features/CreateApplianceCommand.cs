using Appliances.Core;
using Appliances.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Appliances.Api.Features;

public record CreateApplianceCommand(
    Guid UserId,
    Guid TenantId,
    string Name,
    ApplianceType ApplianceType,
    string? Brand,
    string? ModelNumber,
    string? SerialNumber,
    DateTime? PurchaseDate,
    decimal? PurchasePrice) : IRequest<ApplianceDto>;

public class CreateApplianceCommandHandler : IRequestHandler<CreateApplianceCommand, ApplianceDto>
{
    private readonly IAppliancesDbContext _context;
    private readonly ILogger<CreateApplianceCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateApplianceCommandHandler(
        IAppliancesDbContext context,
        ILogger<CreateApplianceCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ApplianceDto> Handle(CreateApplianceCommand request, CancellationToken cancellationToken)
    {
        var appliance = new Appliance
        {
            ApplianceId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            ApplianceType = request.ApplianceType,
            Brand = request.Brand,
            ModelNumber = request.ModelNumber,
            SerialNumber = request.SerialNumber,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            CreatedAt = DateTime.UtcNow
        };

        _context.Appliances.Add(appliance);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishApplianceCreatedEventAsync(appliance);

        _logger.LogInformation("Appliance created: {ApplianceId}", appliance.ApplianceId);

        return appliance.ToDto();
    }

    private Task PublishApplianceCreatedEventAsync(Appliance appliance)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("appliances-events", ExchangeType.Topic, durable: true);

            var @event = new ApplianceCreatedEvent
            {
                UserId = appliance.UserId,
                TenantId = appliance.TenantId,
                ApplianceId = appliance.ApplianceId,
                Name = appliance.Name,
                ApplianceType = appliance.ApplianceType.ToString()
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("appliances-events", "appliance.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ApplianceCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
