using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Destinations;

public record GetDestinationByIdQuery : IRequest<DestinationDto?>
{
    public Guid DestinationId { get; init; }
}

public class GetDestinationByIdQueryHandler : IRequestHandler<GetDestinationByIdQuery, DestinationDto?>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<GetDestinationByIdQueryHandler> _logger;

    public GetDestinationByIdQueryHandler(
        ITravelDestinationWishlistContext context,
        ILogger<GetDestinationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DestinationDto?> Handle(GetDestinationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting destination {DestinationId}", request.DestinationId);

        var destination = await _context.Destinations
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DestinationId == request.DestinationId, cancellationToken);

        return destination?.ToDto();
    }
}
