using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Events;

public record UpdateEventCommand : IRequest<EventDto?>
{
    public Guid EventId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime EventDateTime { get; init; }
    public string? Location { get; init; }
    public bool IsPublic { get; init; }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<UpdateEventCommandHandler> _logger;

    public UpdateEventCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<UpdateEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EventDto?> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating event {EventId}", request.EventId);

        var @event = await _context.Events
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (@event == null)
        {
            _logger.LogWarning("Event {EventId} not found", request.EventId);
            return null;
        }

        @event.Title = request.Title;
        @event.Description = request.Description;
        @event.EventDateTime = request.EventDateTime;
        @event.Location = request.Location;
        @event.IsPublic = request.IsPublic;
        @event.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated event {EventId}", request.EventId);

        return @event.ToDto();
    }
}
