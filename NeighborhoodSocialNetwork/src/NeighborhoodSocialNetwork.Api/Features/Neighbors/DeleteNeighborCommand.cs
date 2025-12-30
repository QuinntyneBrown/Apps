using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Neighbors;

public record DeleteNeighborCommand : IRequest<bool>
{
    public Guid NeighborId { get; init; }
}

public class DeleteNeighborCommandHandler : IRequestHandler<DeleteNeighborCommand, bool>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<DeleteNeighborCommandHandler> _logger;

    public DeleteNeighborCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<DeleteNeighborCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteNeighborCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting neighbor {NeighborId}", request.NeighborId);

        var neighbor = await _context.Neighbors
            .FirstOrDefaultAsync(n => n.NeighborId == request.NeighborId, cancellationToken);

        if (neighbor == null)
        {
            _logger.LogWarning("Neighbor {NeighborId} not found", request.NeighborId);
            return false;
        }

        _context.Neighbors.Remove(neighbor);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted neighbor {NeighborId}", request.NeighborId);

        return true;
    }
}
