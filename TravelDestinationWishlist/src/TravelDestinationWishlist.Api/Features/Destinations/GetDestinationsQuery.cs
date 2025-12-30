using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Destinations;

public record GetDestinationsQuery : IRequest<IEnumerable<DestinationDto>>
{
    public Guid? UserId { get; init; }
    public DestinationType? DestinationType { get; init; }
    public bool? IsVisited { get; init; }
    public string? Country { get; init; }
}

public class GetDestinationsQueryHandler : IRequestHandler<GetDestinationsQuery, IEnumerable<DestinationDto>>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<GetDestinationsQueryHandler> _logger;

    public GetDestinationsQueryHandler(
        ITravelDestinationWishlistContext context,
        ILogger<GetDestinationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DestinationDto>> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting destinations for user {UserId}", request.UserId);

        var query = _context.Destinations.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(d => d.UserId == request.UserId.Value);
        }

        if (request.DestinationType.HasValue)
        {
            query = query.Where(d => d.DestinationType == request.DestinationType.Value);
        }

        if (request.IsVisited.HasValue)
        {
            query = query.Where(d => d.IsVisited == request.IsVisited.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Country))
        {
            query = query.Where(d => d.Country.Contains(request.Country));
        }

        var destinations = await query
            .OrderByDescending(d => d.Priority)
            .ThenBy(d => d.Name)
            .ToListAsync(cancellationToken);

        return destinations.Select(d => d.ToDto());
    }
}
