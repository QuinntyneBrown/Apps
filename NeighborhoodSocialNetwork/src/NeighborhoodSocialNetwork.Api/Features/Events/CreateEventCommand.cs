using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Events;

public record CreateEventCommand : IRequest<EventDto>
{
    public Guid CreatedByNeighborId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime EventDateTime { get; init; }
    public string? Location { get; init; }
    public bool IsPublic { get; init; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<CreateEventCommandHandler> _logger;

    public CreateEventCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<CreateEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating event for neighbor {CreatedByNeighborId}, title: {Title}",
            request.CreatedByNeighborId,
            request.Title);

        var @event = new Event
        {
            EventId = Guid.NewGuid(),
            CreatedByNeighborId = request.CreatedByNeighborId,
            Title = request.Title,
            Description = request.Description,
            EventDateTime = request.EventDateTime,
            Location = request.Location,
            IsPublic = request.IsPublic,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Events.Add(@event);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created event {EventId}", @event.EventId);

        return @event.ToDto();
    }
}
