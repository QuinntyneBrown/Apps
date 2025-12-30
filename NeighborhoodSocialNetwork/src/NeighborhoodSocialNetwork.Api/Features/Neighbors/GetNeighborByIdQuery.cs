using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Neighbors;

public record GetNeighborByIdQuery : IRequest<NeighborDto?>
{
    public Guid NeighborId { get; init; }
}

public class GetNeighborByIdQueryHandler : IRequestHandler<GetNeighborByIdQuery, NeighborDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetNeighborByIdQueryHandler> _logger;

    public GetNeighborByIdQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetNeighborByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NeighborDto?> Handle(GetNeighborByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting neighbor {NeighborId}", request.NeighborId);

        var neighbor = await _context.Neighbors
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.NeighborId == request.NeighborId, cancellationToken);

        if (neighbor == null)
        {
            _logger.LogWarning("Neighbor {NeighborId} not found", request.NeighborId);
            return null;
        }

        return neighbor.ToDto();
    }
}
