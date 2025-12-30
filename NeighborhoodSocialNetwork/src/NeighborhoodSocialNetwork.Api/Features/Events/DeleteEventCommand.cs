using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Events;

public record DeleteEventCommand : IRequest<bool>
{
    public Guid EventId { get; init; }
}

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<DeleteEventCommandHandler> _logger;

    public DeleteEventCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<DeleteEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting event {EventId}", request.EventId);

        var @event = await _context.Events
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (@event == null)
        {
            _logger.LogWarning("Event {EventId} not found", request.EventId);
            return false;
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted event {EventId}", request.EventId);

        return true;
    }
}
