using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Neighbors;

public record GetNeighborsQuery : IRequest<IEnumerable<NeighborDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsVerified { get; init; }
    public string? SearchTerm { get; init; }
}

public class GetNeighborsQueryHandler : IRequestHandler<GetNeighborsQuery, IEnumerable<NeighborDto>>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetNeighborsQueryHandler> _logger;

    public GetNeighborsQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetNeighborsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<NeighborDto>> Handle(GetNeighborsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting neighbors");

        var query = _context.Neighbors.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(n => n.UserId == request.UserId.Value);
        }

        if (request.IsVerified.HasValue)
        {
            query = query.Where(n => n.IsVerified == request.IsVerified.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(n =>
                n.Name.Contains(request.SearchTerm) ||
                (n.Address != null && n.Address.Contains(request.SearchTerm)) ||
                (n.Bio != null && n.Bio.Contains(request.SearchTerm)));
        }

        var neighbors = await query
            .OrderBy(n => n.Name)
            .ToListAsync(cancellationToken);

        return neighbors.Select(n => n.ToDto());
    }
}
