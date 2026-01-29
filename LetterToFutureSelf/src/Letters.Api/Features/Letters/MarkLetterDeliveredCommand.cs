using Letters.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Letters.Api.Features.Letters;

public record MarkLetterDeliveredCommand : IRequest<LetterDto?>
{
    public Guid LetterId { get; init; }
}

public class MarkLetterDeliveredCommandHandler : IRequestHandler<MarkLetterDeliveredCommand, LetterDto?>
{
    private readonly ILettersDbContext _context;
    private readonly ILogger<MarkLetterDeliveredCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public MarkLetterDeliveredCommandHandler(
        ILettersDbContext context,
        ILogger<MarkLetterDeliveredCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<LetterDto?> Handle(MarkLetterDeliveredCommand request, CancellationToken cancellationToken)
    {
        var letter = await _context.Letters
            .FirstOrDefaultAsync(l => l.LetterId == request.LetterId, cancellationToken);

        if (letter == null) return null;

        letter.MarkAsDelivered();
        await _context.SaveChangesAsync(cancellationToken);

        await PublishLetterDeliveredEventAsync(letter, cancellationToken);

        _logger.LogInformation("Letter marked as delivered: {LetterId}", letter.LetterId);

        return new LetterDto
        {
            LetterId = letter.LetterId,
            UserId = letter.UserId,
            Subject = letter.Subject,
            Content = letter.Content,
            WrittenDate = letter.WrittenDate,
            ScheduledDeliveryDate = letter.ScheduledDeliveryDate,
            ActualDeliveryDate = letter.ActualDeliveryDate,
            DeliveryStatus = letter.DeliveryStatus,
            HasBeenRead = letter.HasBeenRead,
            ReadDate = letter.ReadDate,
            CreatedAt = letter.CreatedAt
        };
    }

    private async Task PublishLetterDeliveredEventAsync(Letters.Core.Models.Letter letter, CancellationToken cancellationToken)
    {
        if (_rabbitConnection == null) return;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("letters-events", ExchangeType.Topic, durable: true);

            var @event = new LetterDeliveredEvent
            {
                UserId = letter.UserId,
                TenantId = letter.TenantId,
                LetterId = letter.LetterId,
                DeliveredAt = letter.ActualDeliveryDate ?? DateTime.UtcNow
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("letters-events", "letter.delivered", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish LetterDeliveredEvent");
        }
    }
}
