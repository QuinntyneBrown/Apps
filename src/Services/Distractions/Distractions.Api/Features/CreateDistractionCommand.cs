using Distractions.Core;
using Distractions.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Distractions.Api.Features;

public record CreateDistractionCommand : IRequest<DistractionDto>
{
    public Guid TenantId { get; init; }
    public Guid SessionId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int DurationSeconds { get; init; }
}

public class CreateDistractionCommandHandler : IRequestHandler<CreateDistractionCommand, DistractionDto>
{
    private readonly IDistractionsDbContext _context;
    private readonly ILogger<CreateDistractionCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateDistractionCommandHandler(
        IDistractionsDbContext context,
        ILogger<CreateDistractionCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<DistractionDto> Handle(CreateDistractionCommand request, CancellationToken cancellationToken)
    {
        var distraction = new Distraction(
            request.TenantId,
            request.SessionId,
            request.Type,
            request.Description,
            request.DurationSeconds);

        _context.Distractions.Add(distraction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Distraction created: {DistractionId}", distraction.DistractionId);

        return new DistractionDto
        {
            DistractionId = distraction.DistractionId,
            SessionId = distraction.SessionId,
            Type = distraction.Type,
            Description = distraction.Description,
            OccurredAt = distraction.OccurredAt,
            DurationSeconds = distraction.DurationSeconds
        };
    }
}
