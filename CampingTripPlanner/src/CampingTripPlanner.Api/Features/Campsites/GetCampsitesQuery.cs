using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Campsites;

public record GetCampsitesQuery : IRequest<IEnumerable<CampsiteDto>>
{
    public Guid? UserId { get; init; }
    public CampsiteType? CampsiteType { get; init; }
    public bool? IsFavorite { get; init; }
}

public class GetCampsitesQueryHandler : IRequestHandler<GetCampsitesQuery, IEnumerable<CampsiteDto>>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<GetCampsitesQueryHandler> _logger;

    public GetCampsitesQueryHandler(
        ICampingTripPlannerContext context,
        ILogger<GetCampsitesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CampsiteDto>> Handle(GetCampsitesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting campsites for user {UserId}", request.UserId);

        var query = _context.Campsites.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (request.CampsiteType.HasValue)
        {
            query = query.Where(c => c.CampsiteType == request.CampsiteType.Value);
        }

        if (request.IsFavorite.HasValue)
        {
            query = query.Where(c => c.IsFavorite == request.IsFavorite.Value);
        }

        var campsites = await query
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return campsites.Select(c => c.ToDto());
    }
}
