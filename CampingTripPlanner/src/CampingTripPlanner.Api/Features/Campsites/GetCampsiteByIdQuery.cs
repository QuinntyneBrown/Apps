using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Campsites;

public record GetCampsiteByIdQuery : IRequest<CampsiteDto?>
{
    public Guid CampsiteId { get; init; }
}

public class GetCampsiteByIdQueryHandler : IRequestHandler<GetCampsiteByIdQuery, CampsiteDto?>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<GetCampsiteByIdQueryHandler> _logger;

    public GetCampsiteByIdQueryHandler(
        ICampingTripPlannerContext context,
        ILogger<GetCampsiteByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CampsiteDto?> Handle(GetCampsiteByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting campsite {CampsiteId}", request.CampsiteId);

        var campsite = await _context.Campsites
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CampsiteId == request.CampsiteId, cancellationToken);

        if (campsite == null)
        {
            _logger.LogWarning("Campsite {CampsiteId} not found", request.CampsiteId);
            return null;
        }

        return campsite.ToDto();
    }
}
