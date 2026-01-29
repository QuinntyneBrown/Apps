using Receipts.Core;
using Receipts.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Receipts.Api.Features;

public record CreateReceiptCommand(
    Guid TenantId,
    Guid DeductionId,
    string FileName,
    string ContentType,
    long FileSize,
    string? StoragePath) : IRequest<ReceiptDto>;

public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, ReceiptDto>
{
    private readonly IReceiptsDbContext _context;
    private readonly ILogger<CreateReceiptCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateReceiptCommandHandler(
        IReceiptsDbContext context,
        ILogger<CreateReceiptCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ReceiptDto> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            TenantId = request.TenantId,
            DeductionId = request.DeductionId,
            FileName = request.FileName,
            ContentType = request.ContentType,
            FileSize = request.FileSize,
            StoragePath = request.StoragePath,
            UploadedAt = DateTime.UtcNow
        };

        _context.Receipts.Add(receipt);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishReceiptUploadedEventAsync(receipt);

        _logger.LogInformation("Receipt created: {ReceiptId}", receipt.ReceiptId);

        return receipt.ToDto();
    }

    private Task PublishReceiptUploadedEventAsync(Receipt receipt)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("receipts-events", ExchangeType.Topic, durable: true);

            var @event = new ReceiptUploadedEvent
            {
                TenantId = receipt.TenantId,
                ReceiptId = receipt.ReceiptId,
                DeductionId = receipt.DeductionId,
                FileName = receipt.FileName
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("receipts-events", "receipt.uploaded", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ReceiptUploadedEvent");
        }

        return Task.CompletedTask;
    }
}
