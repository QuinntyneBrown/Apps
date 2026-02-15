using Manuals.Core;
using Manuals.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Manuals.Api.Features;

public record CreateManualCommand(
    Guid TenantId,
    Guid ApplianceId,
    string? Title,
    string? FileUrl,
    string? FileType) : IRequest<ManualDto>;

public class CreateManualCommandHandler : IRequestHandler<CreateManualCommand, ManualDto>
{
    private readonly IManualsDbContext _context;
    private readonly ILogger<CreateManualCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateManualCommandHandler(
        IManualsDbContext context,
        ILogger<CreateManualCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ManualDto> Handle(CreateManualCommand request, CancellationToken cancellationToken)
    {
        var manual = new Manual
        {
            ManualId = Guid.NewGuid(),
            TenantId = request.TenantId,
            ApplianceId = request.ApplianceId,
            Title = request.Title,
            FileUrl = request.FileUrl,
            FileType = request.FileType,
            CreatedAt = DateTime.UtcNow
        };

        _context.Manuals.Add(manual);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishManualUploadedEventAsync(manual);

        _logger.LogInformation("Manual created: {ManualId}", manual.ManualId);

        return manual.ToDto();
    }

    private Task PublishManualUploadedEventAsync(Manual manual)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("manuals-events", ExchangeType.Topic, durable: true);

            var @event = new ManualUploadedEvent
            {
                TenantId = manual.TenantId,
                ManualId = manual.ManualId,
                ApplianceId = manual.ApplianceId,
                Title = manual.Title ?? "Untitled"
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("manuals-events", "manual.uploaded", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ManualUploadedEvent");
        }

        return Task.CompletedTask;
    }
}
