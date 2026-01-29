using Warranties.Core;
using Warranties.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Warranties.Api.Features;

public record CreateWarrantyCommand(
    Guid TenantId,
    Guid ApplianceId,
    string? Provider,
    DateTime? StartDate,
    DateTime? EndDate,
    string? CoverageDetails,
    string? DocumentUrl) : IRequest<WarrantyDto>;

public class CreateWarrantyCommandHandler : IRequestHandler<CreateWarrantyCommand, WarrantyDto>
{
    private readonly IWarrantiesDbContext _context;
    private readonly ILogger<CreateWarrantyCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateWarrantyCommandHandler(
        IWarrantiesDbContext context,
        ILogger<CreateWarrantyCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<WarrantyDto> Handle(CreateWarrantyCommand request, CancellationToken cancellationToken)
    {
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            TenantId = request.TenantId,
            ApplianceId = request.ApplianceId,
            Provider = request.Provider,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CoverageDetails = request.CoverageDetails,
            DocumentUrl = request.DocumentUrl,
            CreatedAt = DateTime.UtcNow
        };

        _context.Warranties.Add(warranty);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishWarrantyCreatedEventAsync(warranty);

        _logger.LogInformation("Warranty created: {WarrantyId}", warranty.WarrantyId);

        return warranty.ToDto();
    }

    private Task PublishWarrantyCreatedEventAsync(Warranty warranty)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("warranties-events", ExchangeType.Topic, durable: true);

            var @event = new WarrantyCreatedEvent
            {
                TenantId = warranty.TenantId,
                WarrantyId = warranty.WarrantyId,
                ApplianceId = warranty.ApplianceId,
                EndDate = warranty.EndDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("warranties-events", "warranty.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish WarrantyCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
