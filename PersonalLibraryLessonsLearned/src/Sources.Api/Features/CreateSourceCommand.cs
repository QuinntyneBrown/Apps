using Sources.Core;
using Sources.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Sources.Api.Features;

public record CreateSourceCommand(
    Guid UserId,
    Guid TenantId,
    string Title,
    string SourceType,
    string? Author,
    string? Url,
    string? Notes) : IRequest<SourceDto>;

public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, SourceDto>
{
    private readonly ISourcesDbContext _context;
    private readonly ILogger<CreateSourceCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateSourceCommandHandler(
        ISourcesDbContext context,
        ILogger<CreateSourceCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<SourceDto> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
    {
        var source = new Source
        {
            SourceId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Title = request.Title,
            SourceType = request.SourceType,
            Author = request.Author,
            Url = request.Url,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Sources.Add(source);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishSourceCreatedEventAsync(source);

        _logger.LogInformation("Source created: {SourceId}", source.SourceId);

        return source.ToDto();
    }

    private Task PublishSourceCreatedEventAsync(Source source)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("sources-events", ExchangeType.Topic, durable: true);

            var @event = new SourceCreatedEvent
            {
                UserId = source.UserId,
                TenantId = source.TenantId,
                SourceId = source.SourceId,
                Title = source.Title,
                SourceType = source.SourceType
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("sources-events", "source.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish SourceCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
