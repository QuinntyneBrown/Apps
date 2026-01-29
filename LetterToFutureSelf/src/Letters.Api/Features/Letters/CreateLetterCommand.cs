using Letters.Core;
using Letters.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Letters.Api.Features.Letters;

public record CreateLetterCommand : IRequest<LetterDto>
{
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public string Subject { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime ScheduledDeliveryDate { get; init; }
}

public class CreateLetterCommandHandler : IRequestHandler<CreateLetterCommand, LetterDto>
{
    private readonly ILettersDbContext _context;
    private readonly ILogger<CreateLetterCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateLetterCommandHandler(
        ILettersDbContext context,
        ILogger<CreateLetterCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<LetterDto> Handle(CreateLetterCommand request, CancellationToken cancellationToken)
    {
        var letter = new Letter
        {
            LetterId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Subject = request.Subject,
            Content = request.Content,
            WrittenDate = DateTime.UtcNow,
            ScheduledDeliveryDate = request.ScheduledDeliveryDate,
            DeliveryStatus = DeliveryStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _context.Letters.Add(letter);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishLetterCreatedEventAsync(letter, cancellationToken);

        _logger.LogInformation("Letter created: {LetterId}", letter.LetterId);

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

    private async Task PublishLetterCreatedEventAsync(Letter letter, CancellationToken cancellationToken)
    {
        if (_rabbitConnection == null) return;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("letters-events", ExchangeType.Topic, durable: true);

            var @event = new LetterCreatedEvent
            {
                UserId = letter.UserId,
                TenantId = letter.TenantId,
                LetterId = letter.LetterId,
                Subject = letter.Subject,
                ScheduledDeliveryDate = letter.ScheduledDeliveryDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("letters-events", "letter.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish LetterCreatedEvent");
        }
    }
}
