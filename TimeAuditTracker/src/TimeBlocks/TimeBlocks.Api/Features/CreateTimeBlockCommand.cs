using TimeBlocks.Core;
using TimeBlocks.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace TimeBlocks.Api.Features;

public record CreateTimeBlockCommand(
    Guid TenantId,
    Guid UserId,
    ActivityCategory Category,
    string Description,
    DateTime StartTime,
    string? Notes,
    string? Tags,
    bool IsProductive) : IRequest<TimeBlockDto>;

public class CreateTimeBlockCommandHandler : IRequestHandler<CreateTimeBlockCommand, TimeBlockDto>
{
    private readonly ITimeBlocksDbContext _context;
    private readonly ILogger<CreateTimeBlockCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateTimeBlockCommandHandler(
        ITimeBlocksDbContext context,
        ILogger<CreateTimeBlockCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<TimeBlockDto> Handle(CreateTimeBlockCommand request, CancellationToken cancellationToken)
    {
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            TenantId = request.TenantId,
            UserId = request.UserId,
            Category = request.Category,
            Description = request.Description,
            StartTime = request.StartTime,
            Notes = request.Notes,
            Tags = request.Tags,
            IsProductive = request.IsProductive,
            CreatedAt = DateTime.UtcNow
        };

        _context.TimeBlocks.Add(timeBlock);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishTimeBlockStartedEventAsync(timeBlock);

        _logger.LogInformation("TimeBlock created: {TimeBlockId}", timeBlock.TimeBlockId);

        return timeBlock.ToDto();
    }

    private Task PublishTimeBlockStartedEventAsync(TimeBlock timeBlock)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("timeblocks-events", ExchangeType.Topic, durable: true);

            var @event = new TimeBlockStartedEvent
            {
                UserId = timeBlock.UserId,
                TenantId = timeBlock.TenantId,
                TimeBlockId = timeBlock.TimeBlockId,
                Category = timeBlock.Category.ToString(),
                StartTime = timeBlock.StartTime
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("timeblocks-events", "timeblock.started", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish TimeBlockStartedEvent");
        }

        return Task.CompletedTask;
    }
}
