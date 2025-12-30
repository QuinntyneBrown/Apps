using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Events;

public record GetEventByIdQuery : IRequest<EventDto?>
{
    public Guid EventId { get; init; }
}

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetEventByIdQueryHandler> _logger;

    public GetEventByIdQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetEventByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EventDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting event {EventId}", request.EventId);

        var @event = await _context.Events
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (@event == null)
        {
            _logger.LogWarning("Event {EventId} not found", request.EventId);
            return null;
        }

        return @event.ToDto();
    }
}
