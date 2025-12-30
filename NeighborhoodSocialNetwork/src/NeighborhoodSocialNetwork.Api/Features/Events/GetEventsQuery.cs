using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Events;

public record GetEventsQuery : IRequest<IEnumerable<EventDto>>
{
    public Guid? CreatedByNeighborId { get; init; }
    public bool? IsPublic { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? SearchTerm { get; init; }
}

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<EventDto>>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetEventsQueryHandler> _logger;

    public GetEventsQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetEventsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting events");

        var query = _context.Events.AsNoTracking();

        if (request.CreatedByNeighborId.HasValue)
        {
            query = query.Where(e => e.CreatedByNeighborId == request.CreatedByNeighborId.Value);
        }

        if (request.IsPublic.HasValue)
        {
            query = query.Where(e => e.IsPublic == request.IsPublic.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(e => e.EventDateTime >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(e => e.EventDateTime <= request.EndDate.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(e =>
                e.Title.Contains(request.SearchTerm) ||
                e.Description.Contains(request.SearchTerm) ||
                (e.Location != null && e.Location.Contains(request.SearchTerm)));
        }

        var events = await query
            .OrderBy(e => e.EventDateTime)
            .ToListAsync(cancellationToken);

        return events.Select(e => e.ToDto());
    }
}
