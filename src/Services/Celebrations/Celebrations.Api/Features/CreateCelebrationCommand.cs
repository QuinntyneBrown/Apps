using Celebrations.Core;
using Celebrations.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Celebrations.Api.Features;

public record CreateCelebrationCommand(
    Guid UserId,
    Guid TenantId,
    string Name,
    string CelebrationType,
    DateTime Date,
    string? Notes,
    bool IsRecurring) : IRequest<CelebrationDto>;

public class CreateCelebrationCommandHandler : IRequestHandler<CreateCelebrationCommand, CelebrationDto>
{
    private readonly ICelebrationsDbContext _context;
    private readonly ILogger<CreateCelebrationCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateCelebrationCommandHandler(
        ICelebrationsDbContext context,
        ILogger<CreateCelebrationCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<CelebrationDto> Handle(CreateCelebrationCommand request, CancellationToken cancellationToken)
    {
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            CelebrationType = request.CelebrationType,
            Date = request.Date,
            Notes = request.Notes,
            IsRecurring = request.IsRecurring,
            CreatedAt = DateTime.UtcNow
        };

        _context.Celebrations.Add(celebration);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishCelebrationCreatedEventAsync(celebration);

        _logger.LogInformation("Celebration created: {CelebrationId}", celebration.CelebrationId);

        return celebration.ToDto();
    }

    private Task PublishCelebrationCreatedEventAsync(Celebration celebration)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("celebrations-events", ExchangeType.Topic, durable: true);

            var @event = new CelebrationCreatedEvent
            {
                UserId = celebration.UserId,
                TenantId = celebration.TenantId,
                CelebrationId = celebration.CelebrationId,
                Name = celebration.Name,
                Date = celebration.Date,
                CelebrationType = celebration.CelebrationType
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("celebrations-events", "celebration.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish CelebrationCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
