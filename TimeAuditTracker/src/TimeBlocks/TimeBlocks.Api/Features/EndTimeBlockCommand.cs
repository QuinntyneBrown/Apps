using TimeBlocks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace TimeBlocks.Api.Features;

public record EndTimeBlockCommand(Guid TimeBlockId) : IRequest<TimeBlockDto?>;

public class EndTimeBlockCommandHandler : IRequestHandler<EndTimeBlockCommand, TimeBlockDto?>
{
    private readonly ITimeBlocksDbContext _context;
    private readonly ILogger<EndTimeBlockCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public EndTimeBlockCommandHandler(
        ITimeBlocksDbContext context,
        ILogger<EndTimeBlockCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<TimeBlockDto?> Handle(EndTimeBlockCommand request, CancellationToken cancellationToken)
    {
        var timeBlock = await _context.TimeBlocks
            .FirstOrDefaultAsync(t => t.TimeBlockId == request.TimeBlockId, cancellationToken);

        if (timeBlock == null) return null;

        timeBlock.EndActivity(DateTime.UtcNow);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishTimeBlockEndedEventAsync(timeBlock);

        _logger.LogInformation("TimeBlock ended: {TimeBlockId}", timeBlock.TimeBlockId);

        return timeBlock.ToDto();
    }

    private Task PublishTimeBlockEndedEventAsync(TimeBlocks.Core.Models.TimeBlock timeBlock)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("timeblocks-events", ExchangeType.Topic, durable: true);

            var @event = new TimeBlockEndedEvent
            {
                UserId = timeBlock.UserId,
                TenantId = timeBlock.TenantId,
                TimeBlockId = timeBlock.TimeBlockId,
                EndTime = timeBlock.EndTime!.Value,
                DurationMinutes = timeBlock.GetDurationInMinutes()!.Value
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("timeblocks-events", "timeblock.ended", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish TimeBlockEndedEvent");
        }

        return Task.CompletedTask;
    }
}
