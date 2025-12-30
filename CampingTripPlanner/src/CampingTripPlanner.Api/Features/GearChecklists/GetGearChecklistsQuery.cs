using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.GearChecklists;

public record GetGearChecklistsQuery : IRequest<IEnumerable<GearChecklistDto>>
{
    public Guid? UserId { get; init; }
    public Guid? TripId { get; init; }
    public bool? IsPacked { get; init; }
}

public class GetGearChecklistsQueryHandler : IRequestHandler<GetGearChecklistsQuery, IEnumerable<GearChecklistDto>>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<GetGearChecklistsQueryHandler> _logger;

    public GetGearChecklistsQueryHandler(
        ICampingTripPlannerContext context,
        ILogger<GetGearChecklistsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GearChecklistDto>> Handle(GetGearChecklistsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gear checklists for user {UserId}", request.UserId);

        var query = _context.GearChecklists.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        if (request.TripId.HasValue)
        {
            query = query.Where(g => g.TripId == request.TripId.Value);
        }

        if (request.IsPacked.HasValue)
        {
            query = query.Where(g => g.IsPacked == request.IsPacked.Value);
        }

        var gearChecklists = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return gearChecklists.Select(g => g.ToDto());
    }
}
