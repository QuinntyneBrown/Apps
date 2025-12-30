using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Neighbors;

public record UpdateNeighborCommand : IRequest<NeighborDto?>
{
    public Guid NeighborId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; }
    public string? ContactInfo { get; init; }
    public string? Bio { get; init; }
    public string? Interests { get; init; }
    public bool IsVerified { get; init; }
}

public class UpdateNeighborCommandHandler : IRequestHandler<UpdateNeighborCommand, NeighborDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<UpdateNeighborCommandHandler> _logger;

    public UpdateNeighborCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<UpdateNeighborCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NeighborDto?> Handle(UpdateNeighborCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating neighbor {NeighborId}", request.NeighborId);

        var neighbor = await _context.Neighbors
            .FirstOrDefaultAsync(n => n.NeighborId == request.NeighborId, cancellationToken);

        if (neighbor == null)
        {
            _logger.LogWarning("Neighbor {NeighborId} not found", request.NeighborId);
            return null;
        }

        neighbor.Name = request.Name;
        neighbor.Address = request.Address;
        neighbor.ContactInfo = request.ContactInfo;
        neighbor.Bio = request.Bio;
        neighbor.Interests = request.Interests;
        neighbor.IsVerified = request.IsVerified;
        neighbor.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated neighbor {NeighborId}", request.NeighborId);

        return neighbor.ToDto();
    }
}
